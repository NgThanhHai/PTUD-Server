package ServerJavaTest.repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import ServerJavaTest.model.Shipper;

@Repository
public interface ShipperRepository extends MongoRepository<Shipper, String>{

}
