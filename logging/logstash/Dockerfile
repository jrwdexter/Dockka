# https://github.com/elastic/logstash-docke
# FROM docker.elastic.co/logstash/logstash:5.4.2
FROM logstash:2.4.1

# Add your logstash plugins setup here
# Example: RUN logstash-plugin install logstash-filter-json
RUN logstash-plugin install logstash-input-zeromq
RUN logstash-plugin install logstash-input-http
RUN logstash-plugin install logstash-filter-elapsed
CMD ["-f", "/usr/share/logstash/pipeline/logstash.conf"]