package ServerJavaTest.controller;

import java.io.OutputStream;
import java.util.List;
import java.util.Optional;

import javax.servlet.http.HttpServletResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import ServerJavaTest.model.Order;
import ServerJavaTest.repository.OrderRepository;

@RestController
@RequestMapping("/api")
public class OrderController {

	@Autowired
	public OrderRepository repo;
	
	public OrderController(OrderRepository repo)
	{
		this.repo = repo;
	}
	public OrderController()
	{
		
	}
	
	
	
	@PostMapping("order/{orderId}/confirm/{confirmerId}")
	public ResponseEntity<HttpStatus> confirmOrder(@PathVariable("orderId") String orderId, @PathVariable("confirmerId") String confirmerId)
	{
		Optional<Order> pendingOrder = repo.findById(orderId);
		if(pendingOrder.isPresent())
		{
			Order order = pendingOrder.get();
			if(order.getStatus() == 2)
			{
				return new ResponseEntity<>(HttpStatus.CONFLICT);
			}else {
				if(order.getCustomer_id() == confirmerId)
				{
					order.setCert_cus(true);
					order.setStatus(order.getStatus() +1 );
					repo.save(order);
					return new ResponseEntity<>(null, HttpStatus.OK);
				}
				if(order.getShop_id() == confirmerId) {
					order.setCert_shop(true);
					order.setStatus(order.getStatus() +1 );
					repo.save(order);
					return new ResponseEntity<>(null, HttpStatus.OK);
				}
				return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
			} 
			
		}else {
			return new ResponseEntity<>(HttpStatus.NO_CONTENT);
		}
	}

}
