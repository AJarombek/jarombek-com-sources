"""
Demonstrate calling an API with the ThreadPoolExecutor & ProcessPoolExecutor classes in the concurrent.futures module.
Author: Andrew Jarombek
Date: 10/3/2020
"""

import time
import os
from concurrent import futures
from typing import List, Tuple

import requests

domain = 'https://jsonplaceholder.typicode.com/'
endpoints = ['posts', 'comments', 'albums', 'photos', 'todos', 'users']


def make_request(endpoint: str) -> requests.Response:
    """
    Make an API request to and endpoint on the globally defined domain name.  Throws an exception if the response has an
    error status code.
    :param endpoint: The endpoint on the API to make a GET request to.
    :return: The response object of the API call.
    """
    url = f'{domain}{endpoint}'
    print(url)
    response: requests.Response = requests.get(url)
    print(response)

    # Raise an error if the HTTP code is 4XX or 500.
    response.raise_for_status()
    return response


def make_requests() -> int:
    """
    Make requests to the API from a pool of threads running concurrently (although not actually, because of Python's
    Global Interpreter Lock [GIL] only allows Python code to run in a single thread at a time.  However, I/O bound tasks
    release the GIL while they wait, allowing ThreadPoolExecutor to be faster than making the API calls synchronously).
    On my machine, this is approximately 2.5x faster than using requests to make API calls sequentially.
    """
    workers = 5
    with futures.ThreadPoolExecutor(workers) as executor:
        # make_request() will be called concurrently from multiple threads.
        res = executor.map(make_request, endpoints)

    return len(list(res))


def make_requests_resolve_as_completed() -> int:
    """
    Same as the previous function except the futures created by the ThreadPoolExecutor are iterated over as they
    resolve, in the order they resolve.
    """
    workers = 5
    futures_list: List[futures.Future] = []
    responses: List[requests.Response] = []

    with futures.ThreadPoolExecutor(workers) as executor:
        for endpoint in endpoints:
            future: futures.Future = executor.submit(make_request, endpoint)
            futures_list.append(future)

        for future in futures.as_completed(futures_list):
            response: requests.Response = future.result()
            responses.append(response)

    return len(responses)


def make_requests_processes() -> int:
    """
    Same operation except using processes (ProcessPoolExecutor) instead of threads (ThreadPoolExecutor).
    """
    cpu_cores = os.cpu_count()
    print(f'Number of CPU cores: {cpu_cores}')

    # ProcessPoolExecutor is generally better for CPU intensive jobs (ex. a large mathematical computation).  It's less
    # useful for a network I/O task like this one.
    with futures.ProcessPoolExecutor() as executor:
        res = executor.map(make_request, endpoints)

    return len(list(res))


def make_requests_with_errors() -> Tuple[int, int]:
    """
    Same as previous ThreadPoolExecutor examples except it handles error codes from the API.
    """
    workers = 5
    futures_list: List[futures.Future] = []
    success_count = 0
    failure_count = 0

    mixed_endpoints = ['posts', 'comments', 'albums', 'invalid', 'i_also_dont_exist']

    with futures.ThreadPoolExecutor(workers) as executor:
        for endpoint in mixed_endpoints:
            future: futures.Future = executor.submit(make_request, endpoint)
            futures_list.append(future)

        for future in futures.as_completed(futures_list):
            try:
                future.result()
                success_count += 1
            except requests.exceptions.HTTPError:
                failure_count += 1

    return success_count, failure_count


def main() -> None:
    start = time.time()
    make_requests()
    end = time.time()
    print(f'API calls from worker threads made in: {end - start}')

    resolved_count = make_requests_resolve_as_completed()
    print(f'Resolved count: {resolved_count}')

    start = time.time()
    make_requests_processes()
    end = time.time()
    print(f'API calls from worker processes made in: {end - start}')

    success_count, failure_count = make_requests_with_errors()
    print(f'Number of successful HTTP requests: {success_count}')
    print(f'Number of unsuccessful HTTP requests: {failure_count}')


if __name__ == '__main__':
    main()
