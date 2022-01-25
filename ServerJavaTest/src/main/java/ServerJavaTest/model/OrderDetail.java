package ServerJavaTest.model;
public class OrderDetail {
    private String ProductID;
    private String Name;
    private int Price;
    private int Quantity;
    private String ImagePath;
    
    
    public String getproduct_id() {
		return ProductID;
	}
	public void setproduct_id(String product_id) {
		this.ProductID = product_id;
	}
	
	public String getproduct_name() {
		return Name;
	}
	public void setproduct_name(String product_name) {
		this.Name = product_name;
	}
	
	public int getproduct_price() {
		return Price;
	}
	public void setproduct_price(int product_price) {
		this.Price = product_price;
	}

	public int getproduct_quantity() {
		return Quantity;
	}
	public void setproduct_quantity(int product_quantity) {
		this.Quantity = product_quantity;
	}
	
	public String getimage_path() {
		return ImagePath;
	}
	public void setimage_path(String image_path) {
		this.ImagePath = image_path;
	}
}