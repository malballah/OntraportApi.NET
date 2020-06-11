﻿using System;
using System.Collections.Generic;
using HanumanInstitute.OntraportApi.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HanumanInstitute.OntraportApi.Models
{
    /// <summary>
    /// An offer needs to be included with each manual transaction. This includes information on products, tax, and shipping.
    /// If invalid information is included in your offer, your request will fail.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ApiTransactionOffer
    {
        /// <summary>
        /// Gets or sets a list of products to include in your offer.
        /// </summary>
        public IList<ApiTransactionProduct> Products { get; private set; } = new List<ApiTransactionProduct>();

        /// <summary>
        /// Gets or sets a list of taxes to be applied to your offer.
        /// </summary>
        public IList<ApiTransactionTax> Taxes { get; private set; } = new List<ApiTransactionTax>();

        /// <summary>
        /// Gets or sets a list of shipping detail to be applied to your offer. 
        /// </summary>
        public IList<ApiTransactionShipping> Shipping { get; private set; } = new List<ApiTransactionShipping>();

        /// <summary>
        /// Gets or sets the number of days to delay the start of the offer.
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// Gets or sets the product total cost before tax, shipping and discounts.
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the total cost of the entire offer.
        /// </summary>
        public decimal GrandTotal { get; set; }

        /// <summary>
        /// Gets or sets whether or not there are applicable taxes for this offer.
        /// </summary>
        [JsonConverter(typeof(JsonConverterBool))]
        public bool HasTaxes { get; set; }

        /// <summary>
        /// Gets or sets whether or not shipping should be applied to this offer.
        /// </summary>
        [JsonConverter(typeof(JsonConverterBool))]
        public bool HasShipping { get; set; }

        /// <summary>
        /// Gets or sets whether or not shipping charges should be applied to recurring orders.
        /// </summary>
        [JsonConverter(typeof(JsonConverterBool))]
        [JsonProperty("shipping_charge_recurring_orders")]
        public bool RecurringShippingCharges { get; set; }

        /// <summary>
        /// For subscriptions, Gets or sets whether or not an invoice should be sent with every recurring order. This value defaults to false.
        /// </summary>
        [JsonConverter(typeof(JsonConverterBool))]
        [JsonProperty("send_recurring_invoice")]
        public bool SendRecurringInvoice { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the credit card on file.
        /// </summary>
        [JsonProperty("ccExpirationDate")]
        public string? CreditCardExpirationDate { get; set; } = string.Empty;



        public ApiTransactionOffer AddProduct(int productId, int quantity, decimal price)
        {
            return Add(new ApiTransactionProduct()
            {
                ProductId = productId,
                Quantity = quantity
            }.AddPrice(price));
        }

        public ApiTransactionOffer Add(ApiTransactionProduct product)
        {
            Products ??= new List<ApiTransactionProduct>();
            Products.Add(product);
            return this;
        }

        public ApiTransactionOffer Add(ApiTransactionTax tax)
        {
            Taxes ??= new List<ApiTransactionTax>();
            Taxes.Add(tax);
            return this;
        }

        public ApiTransactionOffer Add(ApiTransactionShipping shipping)
        {
            Shipping ??= new List<ApiTransactionShipping>();
            Shipping.Add(shipping);
            return this;
        }
    }
}
