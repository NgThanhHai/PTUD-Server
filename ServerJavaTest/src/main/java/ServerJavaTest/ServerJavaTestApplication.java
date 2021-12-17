package ServerJavaTest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.data.mongodb.core.MongoTemplate;

import com.mongodb.ConnectionString;
import com.mongodb.MongoClientSettings;
import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoDatabase;

import ServerJavaTest.repository.CustomerRepository;

@SpringBootApplication
public class ServerJavaTestApplication  {

	
	public static void main(String[] args) {
		SpringApplication.run(ServerJavaTestApplication.class, args);
	}
}
