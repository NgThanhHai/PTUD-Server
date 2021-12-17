package ServerJavaTest.model;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

@Document(collection = "Shipper")
public class Shipper {
	@Id
	public String _id;
	@Field
    public String name;
	@Field
    public String avatar;
	@Field
    public String phone;
	@Field
    public String address;
	@Field
    public String negative_cert;
	@Field
    public String vaccine_cert;
	@Field
    public String identify;
	public String get_id() {
		return _id;
	}
	public void set_id(String _id) {
		this._id = _id;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getAvatar() {
		return avatar;
	}
	public void setAvatar(String avatar) {
		this.avatar = avatar;
	}
	public String getPhone() {
		return phone;
	}
	public void setPhone(String phone) {
		this.phone = phone;
	}
	public String getAddress() {
		return address;
	}
	public void setAddress(String address) {
		this.address = address;
	}
	public String getNegative_cert() {
		return negative_cert;
	}
	public void setNegative_cert(String negative_cert) {
		this.negative_cert = negative_cert;
	}
	public String getVaccine_cert() {
		return vaccine_cert;
	}
	public void setVaccine_cert(String vaccine_cert) {
		this.vaccine_cert = vaccine_cert;
	}
	public String getIdentify() {
		return identify;
	}
	public void setIdentify(String identify) {
		this.identify = identify;
	}
	
	
}
