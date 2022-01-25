package ServerJavaTest.controller;

import java.time.LocalDate;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.Optional;
import java.util.stream.Collectors;

import org.apache.tomcat.util.json.JSONParser;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.cglib.core.CollectionUtils;
import org.springframework.data.mongodb.core.geo.GeoJson;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

import ServerJavaTest.model.Order;
import ServerJavaTest.model.Shipper;
import ServerJavaTest.repository.OrderRepository;
import ServerJavaTest.repository.ShipperRepository;


@RestController
@RequestMapping("/api")
@CrossOrigin(origins = {"http://localhost:8080", "https://ptud.vercel.app/"}, allowedHeaders = "*")
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
	
	
	@GetMapping("/shipper/{shipperId}/salary/month/{month}/year/{year}")
	@CrossOrigin(origins = {"http://localhost:8080", "https://ptud.vercel.app/"}, allowedHeaders = "*")
	public ResponseEntity<JsonObject> getSalary(@PathVariable("shipperId") String shipperId, @PathVariable("month") Integer month, @PathVariable("year") Integer year)
	{
		Optional<Shipper> shipper = repo.findById(shipperId);
		if(!shipper.isPresent())
		{
			return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
		}else {
			List<Order> listOrder = new ArrayList<Order>();
			orderRepo.findAll().forEach(listOrder::add);
			

			CollectionUtils.filter(listOrder, order -> Objects.equals(((Order) order).getShipper_id(),(shipperId)));

			CollectionUtils.filter(listOrder, order -> Objects.equals( ((Order) order).getCreated_at().toInstant().atZone(ZoneId.systemDefault()).toLocalDate().getMonthValue() , month ));
			CollectionUtils.filter(listOrder, order -> Objects.equals( ((Order) order).getCreated_at().toInstant().atZone(ZoneId.systemDefault()).toLocalDate().getYear() , year ));

			if(listOrder.isEmpty())
			{
				return new ResponseEntity<>(HttpStatus.NO_CONTENT);
			}else {
				List<Integer> day = new ArrayList<Integer>();
				;
				for(int i = 0; i < listOrder.size(); i++) {
					if(!day.contains((listOrder.get(i)).getCreated_at().toInstant().atZone(ZoneId.systemDefault()).toLocalDate().getDayOfMonth()))
					{
						day.add((listOrder.get(i)).getCreated_at().toInstant().atZone(ZoneId.systemDefault()).toLocalDate().getDayOfMonth());
					}
				}
				String salary = "{";
				List<Order> newListOrder = new ArrayList<Order>();
				Integer count;
				for(int i = 0; i < day.size(); i++)
				{
					
					Integer currentDay = day.get(i);
					count = 0;

					newListOrder.addAll(listOrder);
					CollectionUtils.filter(newListOrder, order -> Objects.equals( ((Order) order).getCreated_at().toInstant().atZone(ZoneId.systemDefault()).toLocalDate().getDayOfMonth() , currentDay));
					
					for(int j = 0; j < newListOrder.size(); j++)
					{
						
						count += newListOrder.get(j).getShipper_fee();
					}
					if(i == day.size() - 1 )
					{
						salary += "\n" + day.get(i).toString() + " : " + count.toString();
					}else {

						salary += "\n" + day.get(i).toString() + " : " + count.toString() + ",";
					}
				}
				salary += "}";
				JsonObject jsonObject = new Gson().fromJson(salary, JsonObject.class);
				return new ResponseEntity<>(jsonObject, HttpStatus.OK);
			}
		}
	}
}
