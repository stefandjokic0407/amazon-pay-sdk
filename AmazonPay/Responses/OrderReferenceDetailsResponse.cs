﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AmazonPay.Responses
{
    /// <summary>
    /// Documentation Source https://pay.amazon.com/documentation/apireference/201752580
    /// </summary>

    public class OrderReferenceDetailsResponse : AbstractResponse
    {
        private string amazonOrderReferenceId;

        private DateTime expirationTimeStamp;
        private DateTime creationTimestamp;
        private DateTime lastUpdateTimestamp;

        private List<string> constraintID = new List<string>();
        private List<string> description = new List<string>();
        private bool hasConstraint = false;

        private string reasonCode;
        private string reasonDescription;
        private string orderLanguage;

        private string orderReferenceState;
        private string sellerNote;
        private string platformId;

        private string releaseEnvironment;
        private decimal amount;
        private string currencyCode;

        private List<string> authorizationId = new List<string>();

        private string phone;
        private string buyerName;
        private string email;

        private string fullDescriptor;
        private Boolean useAmazonBalanceFirst;

        private string buyerShippingName;
        private string stateOrRegion;
        private string addressLine1;
        private string addressLine2;
        private string addressLine3;
        private string city;
        private string postalCode;
        private string countryCode;
        private string district;
        private string county;
        private string destinationType;

        private string storeName;
        private string sellerOrderId;
        private string customInformation;
        private string supplementaryData;
        private string paymentAuthenticationState;
        private string staticToken;

        /// <summary>
        /// Billing Agreement ID for CreateOrderReferenceForID API call
        /// </summary>
        private string id;
        private string type;

        private string parentKey;

        /// <summary>
        /// Attributes for SetOrderAttributes API call
        /// </summary>
        private bool requestPaymentAuthorization;
        private string paymentServiceProviderId;
        private string paymentServiceProviderOrderId;
        private List<string> orderItemCategories = new List<string>();

        BillingAddressDetails billingAddress;

        /// <summary>
        /// Get the OrderReferenceDetailsResponse
        /// </summary>
        public OrderReferenceDetailsResponse(string xml)
        {
            SetDictionaryAndErrorResponse(xml);
            if (success)
            {
                ParseDictionaryToNewVariables(this.dictionary);
            }
        }

        /// <summary>
        /// Flattening the Dictionary
        /// The input dictionary contains key value pairs in the below format
        /// Type 1. Key (string) , Value (string)
        /// Type 2. Key (string) , Value (Dictionary)
        /// The function will parse the dictionary values into respective class variables by directly jumping to to the switch case for Type 1 
        /// else it will recursively parse the inner dictionary for Type 2
        /// </summary>
        /// <param name="dictionary"></param>
        private void ParseDictionaryToNewVariables(IDictionary dictionary)
        {
            foreach (string strKey in dictionary.Keys)
            {
                // Obj is the value of the dictionary key. this could either be a string or a nested inner dictionary.
                object obj = dictionary[strKey];
                if (obj != null)
                {
                    // If obj is dictionary recursively parse it
                    if (obj is IDictionary)
                    {
                        parentKey = strKey;
                        /* The below if case is when the Billing Address is present. If the key of the parent Dictionary is BillingAddress
                         * then obj being the value of the key will contain the inner Dictionary with Billing Address Details. Send this inner Dictionary
                         * to the BillingAddressDetails class so that it can be seperately flattened and parsed into variables. continue in this case will skip calling the 
                         * ParseDictionaryToVariables function and move to te next element in the parent Dictionary.
                         */
                        if (parentKey.Equals(Operator.BillingAddress.ToString()))
                        {
                            billingAddress = new BillingAddressDetails((IDictionary)obj);
                            continue;
                        }
                        ParseDictionaryToNewVariables((IDictionary)obj);
                    }
                    else
                    {
                        if (Enum.IsDefined(typeof(Operator), strKey))
                        {
                            switch ((Operator)Enum.Parse(typeof(Operator), strKey))
                            {
                                case Operator.AmazonOrderReferenceId:
                                    amazonOrderReferenceId = obj.ToString();
                                    break;
                                case Operator.ExpirationTimestamp:
                                    expirationTimeStamp = DateTime.Parse(obj.ToString());
                                    break;
                                case Operator.RequestId:
                                    requestId = obj.ToString();
                                    break;
                                case Operator.CreationTimestamp:
                                    creationTimestamp = DateTime.Parse(obj.ToString());
                                    break;
                                case Operator.LastUpdateTimestamp:
                                    lastUpdateTimestamp = DateTime.Parse(obj.ToString());
                                    break;
                                case Operator.ReasonCode:
                                    reasonCode = obj.ToString();
                                    break;
                                case Operator.ReasonDescription:
                                    reasonDescription = obj.ToString();
                                    break;
                                case Operator.State:
                                    /* State is the Key in XML that is same for both OrderReferenceStatus state and
                                     * PaymentAuthenticationStatus state. When flattened the XML attribute is lost but
                                     * saved in the parentKey Variable. When parentKey equals 'OrderReferenceStatus'
                                     * then parse it into orderReferenceState else into paymentAuthenticationState
                                     */
                                    if (parentKey.Equals(Operator.OrderReferenceStatus.ToString()))
                                    {
                                        orderReferenceState = obj.ToString();
                                    }
                                    else
                                    {
                                        paymentAuthenticationState = obj.ToString();
                                    }
                                    break;
                                case Operator.SellerNote:
                                    sellerNote = obj.ToString();
                                    break;
                                case Operator.Amount:
                                    amount = decimal.Parse(obj.ToString(), Constants.USNumberFormat);
                                    break;
                                case Operator.CurrencyCode:
                                    currencyCode = obj.ToString();
                                    break;
                                case Operator.PlatformId:
                                    platformId = obj.ToString();
                                    break;
                                case Operator.PostalCode:
                                    postalCode = obj.ToString();
                                    break;
                                case Operator.Name:
                                    /* Name is the Key in XML that is same for both Buyer name and Shipping Address name
                                     * When flattened the XML attribute is lost but saved in the parentKey Variable.
                                     * when parentKey equals 'Buyer' then parse it into buyerName else into buyerShippingName
                                     */
                                    if (parentKey.Equals(Operator.Buyer.ToString()))
                                    {
                                        buyerName = obj.ToString();
                                    }
                                    else
                                    {
                                        buyerShippingName = obj.ToString();
                                    }
                                    break;
                                case Operator.Type:
                                    type = obj.ToString();
                                    break;
                                case Operator.Id:
                                    id = obj.ToString();
                                    break;
                                case Operator.Email:
                                    email = obj.ToString();
                                    break;
                                case Operator.Phone:
                                    phone = obj.ToString();
                                    break;
                                case Operator.FullDescriptor:
                                    fullDescriptor = obj.ToString();
                                    break;
                                case Operator.isAmazonBalanceFirst:
                                    useAmazonBalanceFirst = Boolean.Parse(obj.ToString());
                                    break;
                                case Operator.CountryCode:
                                    countryCode = obj.ToString();
                                    break;
                                case Operator.StateOrRegion:
                                    stateOrRegion = obj.ToString();
                                    break;
                                case Operator.AddressLine1:
                                    addressLine1 = obj.ToString();
                                    break;
                                case Operator.AddressLine2:
                                    addressLine2 = obj.ToString();
                                    break;
                                case Operator.AddressLine3:
                                    addressLine3 = obj.ToString();
                                    break;
                                case Operator.City:
                                    city = obj.ToString();
                                    break;
                                case Operator.County:
                                    county = obj.ToString();
                                    break;
                                case Operator.District:
                                    district = obj.ToString();
                                    break;
                                case Operator.DestinationType:
                                    destinationType = obj.ToString();
                                    break;
                                case Operator.ReleaseEnvironment:
                                    releaseEnvironment = obj.ToString();
                                    break;
                                case Operator.SellerOrderId:
                                    sellerOrderId = obj.ToString();
                                    break;
                                case Operator.CustomInformation:
                                    customInformation = obj.ToString();
                                    break;
                                case Operator.StoreName:
                                    storeName = obj.ToString();
                                    break;
                                /* The below case is when multiple constraints exist in the response. The flattening of the nested Dictionary
                                 * contains JArray of JObjects. Each Jobject contains ConstraintID and it's Description which is parsed and added to the List
                                 */
                                case Operator.Constraint:
                                    JArray array = JArray.Parse(obj.ToString());
                                    hasConstraint = true;
                                    foreach (JObject item in array.Children<JObject>())
                                    {
                                        foreach (JProperty property in item.Properties())
                                        {
                                            string key = property.Name;
                                            string value = property.Value.ToString();
                                            if (key.Equals(Operator.ConstraintID.ToString()))
                                            {
                                                constraintID.Add(value);
                                            }
                                            if (key.Equals(Operator.Description.ToString()))
                                            {
                                                description.Add(value);
                                            }
                                        }
                                    }
                                    break;
                                case Operator.ConstraintID:
                                    constraintID.Add(obj.ToString());
                                    hasConstraint = true;
                                    break;
                                case Operator.Description:
                                    description.Add(obj.ToString());
                                    hasConstraint = true;
                                    break;
                                /* The below case parses two types of Authorization ID member fields. When the nested Dictionary is flattened
                                 * it contains JArray. JArray contains the member field which contains the Authorization ID. this is added to the List
                                 */
                                case Operator.member:
                                    if (obj.GetType() == typeof(JArray))
                                    {
                                        JArray authIdArray = JArray.Parse(obj.ToString());
                                        foreach (string authId in authIdArray)
                                        {
                                            authorizationId.Add(authId);
                                        }
                                    }
                                    else
                                    {
                                        authorizationId.Add(obj.ToString());
                                    }
                                    break;
                                case Operator.OrderLanguage:
                                    orderLanguage = obj.ToString();
                                    break;
                                case Operator.RequestPaymentAuthorization:
                                    requestPaymentAuthorization = Boolean.Parse(obj.ToString());
                                    break;
                                case Operator.PaymentServiceProviderId:
                                    paymentServiceProviderId = obj.ToString();
                                    break;
                                case Operator.PaymentServiceProviderOrderId:
                                    paymentServiceProviderOrderId = obj.ToString();
                                    break;
                                /* The below case parses the order item categories. When the nested Dictionary is flattened
                                  it contains JArray. JArray contains the order item categories. This is added to the List
                                */
                                case Operator.OrderItemCategory:
                                    if (obj.GetType() == typeof(JArray))
                                    {
                                        JArray orderCategoryArray = JArray.Parse(obj.ToString());
                                        foreach (string orderCategory in orderCategoryArray)
                                        {
                                            orderItemCategories.Add(orderCategory);
                                        }
                                    }
                                    else
                                    {
                                       orderItemCategories.Add(obj.ToString());
                                    }
                                    break;
                                case Operator.SupplementaryData:
                                    supplementaryData = obj.ToString();
                                    break;
                                case Operator.StaticToken:
                                    staticToken = obj.ToString();
                                    break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the Amazon OrderReferenceID 
        /// </summary>
        /// <returns>string amazonOrderReferenceId</returns>
        public string GetAmazonOrderReferenceId()
        {
            return this.amazonOrderReferenceId;
        }

        /// <summary>
        /// Get the ExpirationTimestamp
        /// </summary>
        /// <returns>DateTime expirationTimestamp</returns>
        public DateTime GetExpirationTimestamp()
        {
            return this.expirationTimeStamp;
        }

        /// <summary>
        /// Get the CreationTimestamp 
        /// </summary>
        /// <returns>Datetime creationTimestamp</returns>
        public DateTime GetCreationTimestamp()
        {
            return this.creationTimestamp;
        }

        /// <summary>
        /// Get the LastUpdateTimestamp 
        /// </summary>
        /// <returns>Datetime lastUpdateTimestamp</returns>
        public DateTime GetLastUpdateTimestamp()
        {
            return this.lastUpdateTimestamp;
        }

        /// <summary>
        /// Get the ReasonCode 
        /// </summary>
        /// <returns>string reasonCode</returns>
        public string GetReasonCode()
        {
            return this.reasonCode;
        }

        /// <summary>
        /// Get the ReasonDescription 
        /// </summary>
        /// <returns>string reasonDescription</returns>
        public string GetReasonDescription()
        {
            return this.reasonDescription;
        }

        /// <summary>
        /// Get the OrderReferenceState 
        /// </summary>
        /// <returns>string orderReferenceState</returns>
        public string GetOrderReferenceState()
        {
            return this.orderReferenceState;
        }

        /// <summary>
        /// Get the OrderLanguage 
        /// </summary>
        /// <returns>string orderLanguage</returns>
        public string GetOrderLanguage()
        {
            return this.orderLanguage;
        }

        /// <summary>
        /// Get the AddressLine1 
        /// </summary>
        /// <returns>string addressLine1</returns>
        public string GetAddressLine1()
        {
            return this.addressLine1;
        }

        /// <summary>
        /// Get the AddressLine2
        /// </summary>
        /// <returns>string addressLine2</returns>
        public string GetAddressLine2()
        {
            return this.addressLine2;
        }

        /// <summary>
        /// Get the AddressLine3 
        /// </summary>
        /// <returns>string addressLine3</returns>
        public string GetAddressLine3()
        {
            return this.addressLine3;
        }

        /// <summary>
        /// Get the Amount 
        /// </summary>
        /// <returns>decimal amount</returns>
        public decimal GetAmount()
        {
            return this.amount;
        }

        /// <summary>
        /// Get the CurrencyCode 
        /// </summary>
        /// <returns>string currencyCode</returns>
        public string GetCurrencyCode()
        {
            return this.currencyCode;
        }

        /// <summary>
        /// Get the AuthorizationIdList
        /// </summary>
        /// <returns>IList authorizationId</returns>
        public IList<string> GetAuthorizationIdList()
        {
            return this.authorizationId.AsReadOnly();
        }

        /// <summary>
        /// Get the City 
        /// </summary>
        /// <returns>string city</returns>
        public string GetCity()
        {
            return this.city;
        }

        /// <summary>
        /// Get the ConstraintIdList 
        /// </summary>
        /// <returns>IList constraintID</returns>
        public IList<string> GetConstraintIdList()
        {
            return this.constraintID.AsReadOnly();
        }

        /// <summary>
        /// Get the BuyerShippingName 
        /// </summary>
        /// <returns>string buyerShippingName</returns>
        public string GetBuyerShippingName()
        {
            return this.buyerShippingName;
        }

        /// <summary>
        /// Get the CountryCode 
        /// </summary>
        /// <returns>string countryCode</returns>
        public string GetCountryCode()
        {
            return this.countryCode;
        }

        /// <summary>
        /// Get the County 
        /// </summary>
        /// <returns>string county</returns>
        public string GetCounty()
        {
            return this.county;
        }

        /// <summary>
        /// Get the CustomInformation 
        /// </summary>
        /// <returns>string customInformation</returns>
        public string GetCustomInformation()
        {
            return this.customInformation;
        }

        /// <summary>
        /// Get the DescriptionList 
        /// </summary>
        /// <returns>IList description</returns>
        public IList<string> GetDescriptionList()
        {
            return this.description.AsReadOnly();
        }

        /// <summary>
        /// Get the DestinationType 
        /// </summary>
        /// <returns>string destinationType</returns>
        public string GetDestinationType()
        {
            return this.destinationType;
        }

        /// <summary>
        /// Get the District 
        /// </summary>
        /// <returns>string district</returns>
        public string GetDistrict()
        {
            return this.district;
        }

        /// <summary>
        /// Get the Email 
        /// </summary>
        /// <returns>string email</returns>
        public string GetEmail()
        {
            return this.email;
        }

        /// <summary>
        /// Get the HasConstraint 
        /// </summary>
        /// <returns>bool hasConstraint</returns>
        public bool GetHasConstraint()
        {
            return this.hasConstraint;
        }

        /// <summary>
        /// Get the BuyerName 
        /// </summary>
        /// <returns>string buyerName</returns>
        public string GetBuyerName()
        {
            return this.buyerName;
        }

        /// <summary>
        /// Get the Phone 
        /// </summary>
        /// <returns>string phone</returns>
        public string GetPhone()
        {
            return this.phone;
        }

        /// <summary>
        /// Get the FullDescriptor 
        /// </summary>
        /// <returns>string fullDescriptor</returns>
        public string GetFullDescriptor()
        {
            return this.fullDescriptor;
        }

        /// <summary>
        /// Get the AmazonBalanceFirst 
        /// </summary>
        /// <returns>Boolean useAmazonBalanceFirst</returns>
        public Boolean GetAmazonBalanceFirst()
        {
            return this.useAmazonBalanceFirst;
        }

        /// <summary>
        /// Get the PlatformId 
        /// </summary>
        /// <returns>string platformId</returns>
        public string GetPlatformId()
        {
            return this.platformId;
        }

        /// <summary>
        /// Get the PostalCode 
        /// </summary>
        /// <returns>string postalCode</returns>
        public string GetPostalCode()
        {
            return this.postalCode;
        }

        /// <summary>
        /// Get the ReleaseEnvironment 
        /// </summary>
        /// <returns>string releaseEnvironment</returns>
        public string GetReleaseEnvironment()
        {
            return this.releaseEnvironment;
        }

        /// <summary>
        /// Get the SellerNote 
        /// </summary>
        /// <returns>string sellerNote</returns>
        public string GetSellerNote()
        {
            return this.sellerNote;
        }

        /// <summary>
        /// Get the Amazon SellerOrderId 
        /// </summary>
        /// <returns>string sellerOrderId</returns>
        public string GetSellerOrderId()
        {
            return this.sellerOrderId;
        }

        /// <summary>
        /// Get the StateOrRegion 
        /// </summary>
        /// <returns>string stateOrRegion</returns>
        public string GetStateOrRegion()
        {
            return this.stateOrRegion;
        }

        /// <summary>
        /// Get the StoreName 
        /// </summary>
        /// <returns>string storeName</returns>
        public string GetStoreName()
        {
            return this.storeName;
        }

        /// <summary>
        /// Get the BillingAddressDetails 
        /// </summary>
        /// <returns>BillingAddressDetails billingAddress</returns>
        public BillingAddressDetails GetBillingAddressDetails()
        {
            return this.billingAddress;
        }

        /// <summary>
        /// Get the RequestPaymentAuthorization 
        /// </summary>
        /// <returns>bool requestPaymentAuthorization</returns>
        public bool GetRequestPaymentAuthorization()
        {
            return this.requestPaymentAuthorization;
        }

        /// <summary>
        /// Get the PaymentServiceProviderId 
        /// </summary>
        /// <returns>string paymentServiceProviderId</returns>
        public string GetPaymentServiceProviderId()
        {
            return this.paymentServiceProviderId;
        }

        /// <summary>
        /// Get the PaymentServiceProviderOrderId 
        /// </summary>
        /// <returns>string paymentServiceProviderOrderId</returns>
        public string GetPaymentServiceProviderOrderId()
        {
            return this.paymentServiceProviderOrderId;
        }

        /// <summary>
        /// Get the OrderItemCategories 
        /// </summary>
        /// <returns>string orderItemCategories</returns>
        public List<string> GetOrderItemCategories()
        {
            return this.orderItemCategories;
        }

        /// <summary>
        /// Get the SupplemetaryData
        /// </summary>
        /// <returns>string supplementaryData</returns>
        public string GetSupplementaryData()
        {
            return this.supplementaryData;
        }

        /// <summary>
        /// Get the SupplemetaryData
        /// </summary>
        /// <returns>string PaymentAuthenticationState</returns>
        public string GetPaymentAuthenticationState()
        {
            return this.paymentAuthenticationState;
        }

        /// <summary>
        /// Get the StaticToken
        /// </summary>
        /// <returns>string StaticToken</returns>
        public string GetStaticToken()
        {
            return this.staticToken;
        }

    }
}
