package ServerJavaTest.repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import ServerJavaTest.model.Customer;

@Repository
public interface CustomerRepository extends MongoRepository<Customer, String>{

	

}
