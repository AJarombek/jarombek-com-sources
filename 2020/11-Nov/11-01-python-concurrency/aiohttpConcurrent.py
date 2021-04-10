"""
Demonstrate calling an API with asyncio.
Author: Andrew Jarombek
Date: 10/4/2020
"""

import asyncio
import time
from typing import Tuple

import aiohttp


async def make_request(session: aiohttp.ClientSession, endpoint: str) -> Tuple[str, int]:
    """
    Make an API request to and endpoint on a mocked REST API.
    :param session: An aiohttp session interface for making HTTP requests.
    :param endpoint: The endpoint on the API to make a GET request to.
    :return: The response JSON of the API call and the HTTP status code.
    """
    url = f'https://jsonplaceholder.typicode.com/{endpoint}'
    async with session.get(url) as response:
        return await response.json(), response.status


async def make_requests() -> int:
    """
    Make HTTP requests using aiohttp.
    :return: The number of successful API calls made (with 200 HTTP codes).
    """
    endpoints = ['posts', 'comments', 'albums', 'photos', 'todos', 'users']
    success_count = 0

    async with aiohttp.ClientSession() as session:
        for endpoint in endpoints:
            response: Tuple[str, int] = await make_request(session, endpoint)
            print(response[1])

            if response[1] == 200:
                success_count += 1

    return success_count


def main() -> None:
    start = time.time()
    loop = asyncio.get_event_loop()
    success_count = loop.run_until_complete(make_requests())
    end = time.time()
    print(f'API calls completed in: {end - start}')
    print(f'Success count: {success_count}')


if __name__ == '__main__':
    main()
