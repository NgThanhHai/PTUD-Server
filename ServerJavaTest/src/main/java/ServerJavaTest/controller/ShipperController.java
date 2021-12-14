package ServerJavaTest.controller;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import ServerJavaTest.model.Order;
import ServerJavaTest.model.Shipper;
import ServerJavaTest.repository.OrderRepository;
import ServerJavaTest.repository.ShipperRepository;


@RestController
@RequestMapping("/api")
public class ShipperController {
	@Autowired
	public ShipperRepository repo;
	@Autowired
	public OrderRepository orderRepo;
	public ShipperController(ShipperRepository repo, OrderRepository orderRepo)
	{
		this.repo = repo;
		this.orderRepo = orderRepo;
	}
	
	
	@GetMapping("/shipper/{shipperId}/salary")
	public ResponseEntity<List<Order>> getSalary(@PathVariable("shipperId") String shipperId)
	{
		Optional<Shipper> shipper = repo.findById(shipperId);
		if(!shipper.isPresent())
		{
			return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
		}else {
			List<Order> listOrder = new ArrayList<Order>();
			orderRepo.findAll().forEach(listOrder::add);
			
			listOrder.stream()
				      .filter(order -> order.getShipper_id() == shipperId);
			
			if(listOrder.isEmpty())
			{

				return new ResponseEntity<>(HttpStatus.NO_CONTENT);
			}else {
				return new ResponseEntity<>(listOrder, HttpStatus.OK);
			}
		}
	}
}
