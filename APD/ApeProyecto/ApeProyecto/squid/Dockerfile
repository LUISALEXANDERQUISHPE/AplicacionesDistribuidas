FROM ubuntu:latest

RUN apt-get update && apt-get install -y squid

# Configura DNS de Google
#RUN echo "nameserver 8.8.8.8" > /etc/resolv.conf

COPY squid.conf /etc/squid/squid.conf
COPY bad_words.txt /etc/squid/bad_words.txt
COPY blocked_sites.txt /etc/squid/blocked_sites.txt

CMD ["squid", "-N", "-d", "1"]
