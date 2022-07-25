#!/bin/bash

topics=(
flg-study-define-series-failed-event
flg-study-define-series-success-event
flg-study-failed-event
flg-study-ml-researched-event
flg-study-placed-event
flg-study-received-event
flg-study-series-defined-event
flg-study-started-event
flg-study-success-event
flg-study-validate-failed-event
flg-study-validate-success-event
)

for topic in ${topics[@]}; do
  docker exec  kafka-event-bus_kafka-internal_1 kafka-topics --bootstrap-server kafka-internal:29092 --create --topic $topic
done