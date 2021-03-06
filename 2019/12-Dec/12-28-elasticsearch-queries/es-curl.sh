#!/usr/bin/env bash

# Commands for making queries against the ElasticSearch server
# Author: Andrew Jarombek
# Date: 12/27/2019

ES_ENDPOINT=https://search-sandbox-elasticsearch-demo-krzp4p5roknptbcoow2xlb5sby.us-east-1.es.amazonaws.com

# Delete, create, and retrieve an index for a race on ElasticSearch.
curl -XDELETE ${ES_ENDPOINT}/race
curl -XPUT ${ES_ENDPOINT}/race -H 'Content-Type: application/json' -d @data/race/index.json
curl ${ES_ENDPOINT}/race?pretty=true

# Delete the documents in the race index
curl -XDELETE ${ES_ENDPOINT}/race/_doc/1
curl -XDELETE ${ES_ENDPOINT}/race/_doc/2
curl -XDELETE ${ES_ENDPOINT}/race/_doc/3
curl -XDELETE ${ES_ENDPOINT}/race/_doc/4
curl -XDELETE ${ES_ENDPOINT}/race/_doc/5
curl -XDELETE ${ES_ENDPOINT}/race/_doc/6
curl -XDELETE ${ES_ENDPOINT}/race/_doc/7
curl -XDELETE ${ES_ENDPOINT}/race/_doc/8
curl -XDELETE ${ES_ENDPOINT}/race/_doc/9

# Add documents to the race index
curl -XPUT ${ES_ENDPOINT}/race/_doc/1 -H 'Content-Type: application/json' -d @data/race/race1.json
curl -XPUT ${ES_ENDPOINT}/race/_doc/2 -H 'Content-Type: application/json' -d @data/race/race2.json
curl -XPUT ${ES_ENDPOINT}/race/_doc/3 -H 'Content-Type: application/json' -d @data/race/race3.json
curl -XPUT ${ES_ENDPOINT}/race/_doc/4 -H 'Content-Type: application/json' -d @data/race/race4.json
curl -XPUT ${ES_ENDPOINT}/race/_doc/5 -H 'Content-Type: application/json' -d @data/race/race5.json
curl -XPUT ${ES_ENDPOINT}/race/_doc/6 -H 'Content-Type: application/json' -d @data/race/race6.json
curl -XPUT ${ES_ENDPOINT}/race/_doc/7 -H 'Content-Type: application/json' -d @data/race/race7.json
curl -XPUT ${ES_ENDPOINT}/race/_doc/8 -H 'Content-Type: application/json' -d @data/race/race8.json
curl -XPUT ${ES_ENDPOINT}/race/_doc/9 -H 'Content-Type: application/json' -d @data/race/race9.json

# Retrieve the documents in the race index
curl ${ES_ENDPOINT}/race/_doc/1?pretty=true
curl ${ES_ENDPOINT}/race/_doc/2?pretty=true
curl ${ES_ENDPOINT}/race/_doc/3?pretty=true
curl ${ES_ENDPOINT}/race/_doc/4?pretty=true
curl ${ES_ENDPOINT}/race/_doc/5?pretty=true
curl ${ES_ENDPOINT}/race/_doc/6?pretty=true
curl ${ES_ENDPOINT}/race/_doc/7?pretty=true
curl ${ES_ENDPOINT}/race/_doc/8?pretty=true
curl ${ES_ENDPOINT}/race/_doc/9?pretty=true

# Query all the documents in the race index
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' -d @query/00-race-all.json

# Query for races that have a mile field with a value between 1 and 2, inclusive.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' -d @query/01-race-short.json

# Query for short races (1-2 miles) in January.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' -d @query/02-race-short-jan.json

# Query for races in the next 15 days and the last 15 days.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/03-race-rolling-month.json

# Query for races that have a 'meters' field.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' -d @query/04-race-meters.json

# Query for races that have a category that exactly matches 'Road'.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' -d @query/05-race-road.json

# Same as the previous query, except no scoring occurs [all documents get a score of 1.0].
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/06-race-road-no-score.json

# Perform a full text search on a 'text' field.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/07-race-match-text.json

# Perform a full text search on a 'keyword' field.  This is the same as performing a term query (05-race-road)
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/08-race-match-keyword.json

# Perform a full text search for races that have a category containing the keywords 'Road' or 'Trail'.
# The 'or' is implicit.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/09-race-road-trail.json

# Perform a full text search for races that have a category containing the keywords 'Road' AND 'Trail'
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/10-race-road-and-trail.json

# Perform a full text search for races that have a category containing the keywords 'Road' OR 'Trail'
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/11-race-road-or-trail.json

# Query races whose names contain two of the following strings: 'NYRR', '#1', '#2', '#3'.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/12-race-nyrr-first-three.json

# Fuzzy match query of 'NYCRR' will match documents with 'NYRR' in their name.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/13-race-nyrr-fuzzy.json

# Phrase match query of 'York NY' will match documents whose location ends in 'York, NY'.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/14-race-ny-phrase.json

# Phrase match query that allows for two missing words in the search text phrase.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/15-race-phrase-slop.json

# Query match on multiple fields with a boost on the first field - 'title' and 'description'
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/16-race-multi-match.json

# Bool Query that returns a mile race at the armory that occurs in March 2020.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/17-race-mile-armory-march.json

# Mile races at the armory without any scoring.  All scores are set to 0.0.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/18-race-mile-armory-no-score.json

# Mile races either at the armory or in 2020.  This is an inclusive OR.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/19-race-mile-2020.json

# Query races at the armory that are not the mile.
curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' \
    -d @query/20-race-armory-not-mile.json

curl ${ES_ENDPOINT}/race/_doc/_search?pretty=true -H 'Content-Type: application/json' -d '{}'