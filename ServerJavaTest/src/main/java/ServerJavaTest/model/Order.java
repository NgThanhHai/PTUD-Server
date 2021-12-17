package ServerJavaTest.model;

import java.security.Timestamp;
import java.util.Date;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

@Document(collection = "Order")
public class Order {

	@Id
	public String _id;
	@Field
	public int total;
	@Field
	public int status;
	@Field
	public String shop_id;
	@Field
	public String customer_id;
	@Field
	public Date created_at;
	@Field
	public Date updated_at;
	@Field
	public Boolean cert_shop;
	@Field
	public Boolean cert_cus;
	@Field
	public String shipper_id;
	@Field
	public int shipper_fee;
	
	public int getShipper_fee() {
		return shipper_fee;
	}
	public void setShipper_fee(int shipper_fee) {
		this.shipper_fee = shipper_fee;
	}
	public String get_id() {
		return _id;
	}
	public void set_id(String _id) {
		this._id = _id;
	}
	public int getTotal() {
		return total;
	}
	public void setTotal(int total) {
		this.total = total;
	}
	public int getStatus() {
		return status;
	}
	public void setStatus(int status) {
		this.status = status;
	}
	public String getShop_id() {
		return shop_id;
	}
	public void setShop_id(String shop_id) {
		this.shop_id = shop_id;
	}
	public String getCustomer_id() {
		return customer_id;
	}
	public void setCustomer_id(String customer_id) {
		this.customer_id = customer_id;
	}
	public Date getCreated_at() {
		return created_at;
	}
	public void setCreated_at(Date created_at) {
		this.created_at = created_at;
	}
	public Date getUpdated_at() {
		return updated_at;
	}
	public void setUpdated_at(Date updated_at) {
		this.updated_at = updated_at;
	}
	public Boolean getCert_shop() {
		return cert_shop;
	}
	public void setCert_shop(Boolean cert_shop) {
		this.cert_shop = cert_shop;
	}
	public Boolean getCert_cus() {
		return cert_cus;
	}
	public void setCert_cus(Boolean cert_cus) {
		this.cert_cus = cert_cus;
	}
	public String getShipper_id() {
		return shipper_id;
	}
	public void setShipper_id(String shipper_id) {
		this.shipper_id = shipper_id;
	}
	
	public Order()
	{
		super();
	}
}
