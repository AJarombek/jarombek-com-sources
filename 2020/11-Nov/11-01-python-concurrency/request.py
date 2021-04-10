"""
Demonstrate calling an API with the requests library.  Uses a free mock REST API.
Author: Andrew Jarombek
Date: 10/3/2020
"""

import time

import requests

domain = 'https://jsonplaceholder.typicode.com/'
endpoints = ['posts', 'comments', 'albums', 'photos', 'todos', 'users']


def make_requests():
    for endpoint in endpoints:
        url = f'{domain}{endpoint}'
        print(url)
        response = requests.get(url)
        print(response.status_code)


def main():
    start = time.time()
    make_requests()
    end = time.time()
    print(f'API calls made in: {end - start}')


if __name__ == '__main__':
    main()
