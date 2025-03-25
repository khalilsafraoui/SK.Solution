FROM rabbitmq:4.0.7

# Expose the default RabbitMQ ports
EXPOSE 5672 15672

# Enable the RabbitMQ management plugin
RUN rabbitmq-plugins enable --offline rabbitmq_management

# Set environment variables (optional, modify as needed)
ENV RABBITMQ_DEFAULT_USER=admin
ENV RABBITMQ_DEFAULT_PASS=admin
ENV RABBITMQ_DEFAULT_VHOST=/

# Define the command to run RabbitMQ
CMD ["rabbitmq-server"]