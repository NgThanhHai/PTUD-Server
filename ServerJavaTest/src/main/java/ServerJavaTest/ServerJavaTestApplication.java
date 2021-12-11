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
public class ServerJavaTestApplication  implements CommandLineRunner {

	@Autowired
	CustomerRepository repository;
	
	public static void main(String[] args) {
		SpringApplication.run(ServerJavaTestApplication.class, args);
		
//		ConnectionString connectionString = new ConnectionString("mongodb+srv://ptud:ptud@cluster0.7gw7q.mongodb.net/test?retryWrites=true&w=majority");
//		MongoClientSettings settings = MongoClientSettings.builder()
//        .applyConnectionString(connectionString)
//        .build();
//		MongoClient mongoClient = MongoClients.create(settings);
//		MongoDatabase database = mongoClient.getDatabase("PTUD");
//		
//		try {
//			System.out.println(database.listCollectionNames());
//		} catch (Exception e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		}
	}

	@Override
	public void run(String... args) throws Exception {
		listAll();
	}
	public void listAll() {
		System.out.println("Listing data");
		repository.findAll().forEach(u -> System.out.println(u));
	}	
}
