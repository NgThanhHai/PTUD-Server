package ServerJavaTest.controller;

import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import ServerJavaTest.model.Customer;
import ServerJavaTest.repository.CustomerRepository;


@RestController
@RequestMapping("/api")
public class CustomerController {
	
	@Autowired
	public CustomerRepository repo;
	
	@Autowired
	public CustomerController(CustomerRepository repo)
	{
		this.repo = repo;
	}
	
	@GetMapping("/test")
	public ResponseEntity<HttpStatus> TestApi() {
		return new ResponseEntity<>(null, HttpStatus.OK);
	}
	
	@GetMapping("/customer")
	public ResponseEntity<List<Customer>> GetAllCustomer() {
		
		try {
			List<Customer> listCustomer = new ArrayList<Customer>();
			repo.findAll().forEach(listCustomer::add);
			if(listCustomer.isEmpty())
			{
				return new ResponseEntity<>(HttpStatus.NO_CONTENT);
			}
			
			return new ResponseEntity<>(listCustomer, HttpStatus.OK);
			}
		catch(Exception e)
		{
			return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
		}
	}
}
