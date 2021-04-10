"""
Tests for the Python concurrency code using concurrent.futures, asyncio, and aiohttp.
Author: Andrew Jarombek
Date: 10/4/2020
"""

import unittest
import asyncio

from request import make_requests as make_synchronous_requests
from concurrentFutures import make_requests, make_requests_resolve_as_completed, make_requests_with_errors, \
    make_requests_processes
from asyncioCoroutines import running_predictions, running_predictions_concurrent, running_predictions_deprecated
from aiohttpConcurrent import make_requests as make_requests_aio_http


class RequestTests(unittest.TestCase):

    def test_make_requests(self):
        try:
            make_synchronous_requests()
        except:
            self.fail("Synchronous API requests with the 'requests' library should not fail.")


class ConcurrentFuturesTests(unittest.TestCase):

    def test_make_requests(self):
        self.assertEqual(6, make_requests())

    def test_make_requests_resolve_as_completed(self):
        self.assertEqual(6, make_requests_resolve_as_completed())

    def test_make_requests_with_errors(self):
        self.assertEqual((3, 2), make_requests_with_errors())

    def test_make_requests_processes(self):
        self.assertEqual(6, make_requests_processes())


class AsyncioTests(unittest.TestCase):

    def test_running_predictions(self):
        sunday_run, weekly_mileage = asyncio.run(running_predictions())
        self.assertEqual(12.31, sunday_run)
        self.assertEqual(44, weekly_mileage)

    def test_running_predictions_concurrent(self):
        predictions = asyncio.run(running_predictions_concurrent())
        self.assertEqual(44, predictions[0])
        self.assertEqual(12.31, predictions[1])

    def running_predictions_deprecated(self):
        try:
            loop = asyncio.get_event_loop()
        except:
            loop = asyncio.new_event_loop()

        sunday_run, weekly_mileage = loop.run_until_complete(running_predictions_deprecated())
        self.assertEqual(13.5, sunday_run)
        self.assertEqual(48, weekly_mileage)


class AioHTTPTests(unittest.TestCase):

    def test_make_requests(self):
        loop = asyncio.get_event_loop()
        success_count = loop.run_until_complete(make_requests_aio_http())
        self.assertEqual(6, success_count)


if __name__ == '__main__':
    unittest.main()
