﻿using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;


public interface IUnitedPharmaService
{
    [OperationContract]
    double CostOfSandwiches(int quantity);
}