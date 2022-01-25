package ServerJavaTest.model;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

@Document(collection = "OrderDetail")
public class OrderDetail {
	@Id
	public String _id;
	@Field
	public String order_id;
	@Field
	public String product;
	@Field
	public int quantity;
	@Field
	public int price;
	
	public OrderDetail(String _id, String order_id, String product, int quantity, int price) {
		super();
		this._id = _id;
		this.order_id = order_id;
		this.product = product;
		this.quantity = quantity;
		this.price = price;
	}
	
	public String get_id() {
		return _id;
	}
	public void set_id(String _id) {
		this._id = _id;
	}
	public String getOrder_id() {
		return order_id;
	}
	public void setOrder_id(String order_id) {
		this.order_id = order_id;
	}
	public String getProduct() {
		return product;
	}
	public void setProduct(String product) {
		this.product = product;
	}
	public int getQuantity() {
		return quantity;
	}
	public void setQuantity(int quantity) {
		this.quantity = quantity;
	}
	public int getPrice() {
		return price;
	}
	public void setPrice(int price) {
		this.price = price;
	}
}
