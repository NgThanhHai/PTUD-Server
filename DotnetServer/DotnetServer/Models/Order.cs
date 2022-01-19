using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DotnetServer.Models
{

    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string shop_id { get; set; }
        public string customer_id { get; set; }
        public string shipper_id { get; set; }
        public string ship_info { get; set; }
        public int? status { get; set; }
        public int? total { get; set; }
        public double? shipper_fee { get; set; }
        public bool cert_shop { get; set; }
        public bool cert_cus { get; set; }
        [BsonDateTimeOptions]
        public DateTime created_at { get; set; }
        [BsonDateTimeOptions]
        public DateTime updated_at { get; set; }
        public List<OrderDetail> order_detail  { get; set; }

}

    public class orderBodyRequest
    {
        public string shop_id { get; set; }
        public string customer_id { get; set; }
        public string shipper_id { get; set; }
        public int? status { get; set; }
        public string ship_info { get; set; }
        [BsonDateTimeOptions]
        public DateTime? from { get; set; } 
        [BsonDateTimeOptions]
        public DateTime? to { get; set; }
    }

    public class OrderResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string customer_id { get; set; }
        public string cus_name { get; set; }
        public string shipper_id { get; set; }
        public string ship_info { get; set; }
        public int? status { get; set; }
        public int? total { get; set; }
        public double? shipper_fee { get; set; }
        public bool cert_shop { get; set; }
        public bool cert_cus { get; set; }
        [BsonDateTimeOptions]
        public DateTime created_at { get; set; }
        [BsonDateTimeOptions]
        public DateTime updated_at { get; set; }
        public List<OrderDetail> order_detail { get; set; }
    }
}
