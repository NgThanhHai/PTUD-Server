using DotnetServer.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetServer.Services
{


    public class OrderService
    {
        private readonly IMongoCollection<Order> _orderCollection;
        private readonly ShopService _shopService;
        private readonly CustomerService _customerService;
        public OrderService(IDatabaseSettings settings, ShopService ShopService, CustomerService CustomerService)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

            _orderCollection = mongoDatabase.GetCollection<Order>("Order");
            _shopService = ShopService;
            _customerService = CustomerService;
        }

        public List<OrderResponse> Get(orderBodyRequest request)
        {
            var result = new List<OrderResponse>();
            var dummy = _orderCollection.Find(x =>

             (request.shop_id == null || x.shop_id == request.shop_id)
             && (request.shipper_id == null || x.shipper_id == request.shipper_id)
             && (request.status == null || x.status == request.status)
             && ((request.from == null && request.to == null)
                    || (request.from == null && request.to != null && x.created_at <= request.to)
                    || (request.from != null && request.to == null && x.created_at >= request.from)
                    || (request.from != null && request.to != null && request.from >= x.created_at && x.created_at <= request.to)
             )
             ).ToList().OrderByDescending(x => x.created_at).ToList();

            for (int i = 0; i < dummy.Count; i++)
            {
                var cus = _customerService.Get(dummy[i].customer_id);
                var shop = _shopService.Get(dummy[i].shop_id);
                if (cus == null || shop == null)
                {
                    continue;
                }

                var curValue = dummy[i];
                var resultDummy = new OrderResponse();
                resultDummy._id = curValue._id;
                resultDummy.shop_id = curValue.shop_id;
                resultDummy.customer_id = curValue.customer_id;
                resultDummy.shipper_id = curValue.shipper_id;
                resultDummy.status = curValue.status;
                resultDummy.total = curValue.total;
                resultDummy.shipper_fee = curValue.shipper_fee;
                resultDummy.cert_shop = curValue.cert_shop;
                resultDummy.cert_cus = curValue.cert_cus;
                resultDummy.created_at = curValue.created_at;
                resultDummy.updated_at = curValue.updated_at;
                resultDummy.ship_info = curValue.ship_info;
                resultDummy.shop_name = shop.name;
                resultDummy.cus_name = cus.name;
                resultDummy.order_detail = curValue.order_detail;
                result.Add(resultDummy);
            }

            return result;
        }




        public OrderResponse Get(string id) {
            var curValue = _orderCollection.Find(x => x._id == id).FirstOrDefault();

            var cus = _customerService.Get(curValue.customer_id);
            var shop = _shopService.Get(curValue.shop_id);
            var resultDummy = new OrderResponse();

            if (cus == null || shop == null)
            {
                return resultDummy;
            }

            resultDummy._id = curValue._id;
            resultDummy.shop_id = curValue.shop_id;
            resultDummy.customer_id = curValue.customer_id;
            resultDummy.shipper_id = curValue.shipper_id;
            resultDummy.status = curValue.status;
            resultDummy.total = curValue.total;
            resultDummy.shipper_fee = curValue.shipper_fee;
            resultDummy.cert_shop = curValue.cert_shop;
            resultDummy.cert_cus = curValue.cert_cus;
            resultDummy.created_at = curValue.created_at;
            resultDummy.updated_at = curValue.updated_at;
            resultDummy.ship_info = curValue.ship_info;
            resultDummy.shop_name = shop.name;
            resultDummy.cus_name = cus.name;
            resultDummy.order_detail = curValue.order_detail;
            return resultDummy;
        }
        public Order GetOrigin(string id) => _orderCollection.Find(x => x._id == id).FirstOrDefault();
        public Order Create(Order newOrder)
        {
            var dummy = newOrder;
            dummy.created_at = DateTime.Now;
            dummy.updated_at = DateTime.Now;
            _orderCollection.InsertOne(dummy);
            return newOrder;
        }

        public Order Update(string id, Order updatedOrder)
        {
            _orderCollection.ReplaceOne(x => x._id == id, updatedOrder);
            return updatedOrder;
        }


        public void Remove(string id) =>
             _orderCollection.DeleteOne(x => x._id == id);
    }
}