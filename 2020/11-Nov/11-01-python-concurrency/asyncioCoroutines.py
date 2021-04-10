"""
Demonstrate calling an API with asyncio.
Author: Andrew Jarombek
Date: 10/3/2020
"""

import asyncio
import time
from typing import Tuple, List, Generator, Any

"""
Asyncio example in Python 3.5+
"""


async def predicted_sunday_run_length() -> float:
    """
    Simulate a long running operation that predicts the length of my Sunday long run.
    """
    await asyncio.sleep(1)
    return 12.31


async def predicted_weekly_mileage() -> float:
    """
    Simulate a long running operation that predicts my weekly running mileage.
    """
    await asyncio.sleep(2)
    return 44


async def running_predictions() -> Tuple[float, float]:
    """
    Using async/await, call both the simulated functions.
    """
    sunday_run = await predicted_sunday_run_length()
    weekly_mileage = await predicted_weekly_mileage()
    return sunday_run, weekly_mileage


async def running_predictions_concurrent() -> List[float]:
    """
    The same as running_predictions() above, except it uses asyncio.gather() instead of two await statements.
    """
    return await asyncio.gather(predicted_weekly_mileage(), predicted_sunday_run_length())


def main() -> None:
    """
    Using asyncio, call the async function running_predictions.
    """
    start = time.time()
    sunday_run, weekly_mileage = asyncio.run(running_predictions())
    end = time.time()
    print(sunday_run)
    print(weekly_mileage)

    # Two awaits completes in 3 seconds.
    print(f'two awaits completes in: {end - start}')

    start = time.time()
    predictions = asyncio.run(running_predictions_concurrent())
    end = time.time()
    print(predictions[0])
    print(predictions[1])

    # await asyncio.gather completes in 2 seconds.
    print(f'await asyncio.gather() completes in: {end - start}')


"""
Asyncio example in Python 3.4.
This is the same as Python 3.5+ except the @asyncio.coroutine annotation is used instead of the async function syntax, 
and 'yield from' is used instead of the await keyword.
"""


@asyncio.coroutine
def predicted_sunday_run_length_deprecated() -> float:
    yield from asyncio.sleep(1)
    return 13.5


@asyncio.coroutine
def predicted_weekly_mileage_deprecated() -> float:
    yield from asyncio.sleep(2)
    return 48


@asyncio.coroutine
def running_predictions_deprecated() -> Generator[Any, Any, Tuple[float, float]]:
    sunday_run = yield from predicted_sunday_run_length()
    weekly_mileage = yield from predicted_weekly_mileage()
    return sunday_run, weekly_mileage


def main_deprecated() -> None:
    start = time.time()
    try:
        loop = asyncio.get_event_loop()
    except:
        loop = asyncio.new_event_loop()

    sunday_run, weekly_mileage = loop.run_until_complete(running_predictions_deprecated())
    loop.close()
    end = time.time()

    print(sunday_run)
    print(weekly_mileage)

    # Two awaits in Python 3.4 also completes in 3 seconds.
    print(f'two awaits (python 3.4) completes in: {end - start}')


if __name__ == '__main__':
    main()
    main_deprecated()
