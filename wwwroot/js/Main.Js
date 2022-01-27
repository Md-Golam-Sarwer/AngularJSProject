
///// <reference path="../lib/angular.js/angular.js" />
///// <reference path="../lib/angular-route/angular-route.js" />
//To define Angular
var app = angular.module('MainModule', []);
//Get API Address

var baseUrl = 'https://localhost:44366/Purchase/GetPurchases/';
var baseUrl1 = 'https://localhost:44366/Purchase/DeletePurchase/';
var baseUrl2 = 'https://localhost:44366/Purchase/AddPurchase';


app.factory('PurchaseFact', function ($http) {
    var factory = {};
    factory.GetPurchases = function () {
        return $http.get(baseUrl);
    }
    factory.SavePurchases = function (obj) {
        return $http.post(baseUrl2, obj);
    }
    factory.UpdatePurchases = function (obj) {
        return $http.put(baseUrl + obj.id, obj);
    }
    factory.DeletePurchases = function (id) {
        return $http.delete(baseUrl + id);
    }
    return factory;
});
app.controller('PurchaseCtrl', function ($scope, PurchaseFact) {
    Init();
    function Init() {

        PurchaseFact.GetPurchases().then(function (res) {
            $scope.PurchaseList = res.data;
        })
    }
    $scope.SavePurchase = function () {
        PurchaseFact.SavePurchases($scope.objPurchase).then(function () {
            Init();
            Clear();
        })
    }
    $scope.EditPurchase = function (p) {
        $scope.objPurchase = p;
    }

    $scope.UpdatePurchase = function () {
        PurchaseFact.UpdatePurchases($scope.objPurchase).then(function () {
            Init();
            Clear();
        })
    }
    $scope.DeletePurchase = function (id) {
        PurchaseFact.DeletePurchases(id).then(function () {
            Init();
        })
    }
    function Clear() {
        $scope.objPurchase = null;
    }
});