1. Build the Docker Image
Make sure you are in the same directory as rabbitmq.dockerfile and run:    docker build -t my-rabbitmq -f rabbitmq.dockerfile .

2. Run the RabbitMQ Container
After the image is built, run:       docker run -d --name rabbitmq-container -p 5672:5672 -p 15672:15672 my-rabbitmq

3. Verify the Container is Running
Check if the container is up and running:       docker ps

4. Access the RabbitMQ Management UI
Open your browser and go to:     http://localhost:15672/


Username: admin
Password: admin

