/// <reference path="../lib/angular.js/angular.js" />
/// <reference path="../lib/angular-route/angular-route.js" />

angular.module("EmployerApp", []).controller("EmployerCtrl", ['$scope',
    function ($scope) {
        $scope.EmployerArray = [];
        $scope.load;

        $scope.load = function () {
            $.ajax({
                type: 'GET',
                contentType: 'application/json',
                url: '/Employer/GetEmployers',
                success: function (data) {
                    $scope.EmployerArray = data;
                    $scope.$apply();
                }
            });
        };

        $scope.BranchArray = [];
        $scope.getBranchList = function () {
            $.ajax({
                type: 'GET',
                contentType: 'application/json',
                url: '/Branch/GetBranches',
                success: function (data) {
                    $scope.BranchArray = data;
                    $scope.$apply();
                }
            });
        };


        $scope.checkselection = function () {
            if ($scope.branchID != "" && $scope.branchID != undefined) {
                $scope.msg = 'Selected Value : ' + $scope.branchID;
            }
            else {
                $scope.msg = 'Please Select Dropdown Value';
            }
        }

        $scope.load();
        $scope.employerInfo = { employeeID: '', employeeName: '', salary: '', contactAddress: '', mobileNo: '', dob: '', isActive: '', branchID: '' };
        $scope.clear = function () {
            $scope.employerInfo.employeeID = '';
            $scope.employerInfo.employeeName = '';
            $scope.employerInfo.salary = '';
            $scope.employerInfo.contactAddress = '';
            $scope.employerInfo.mobileNo = '';
            $scope.employerInfo.dob = '';
            $scope.employerInfo.isActive = '';
            $scope.employerInfo.branchID = '';
        };

        //Add
        $scope.employerInsert = function (employerInfo) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json',
                url: '/Employer/AddEmployer',
                data: JSON.stringify(employerInfo),
                success: function (data) {
                    $scope.EmployerArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Update
        $scope.updateStart = function (employerInfo) {
            $scope.employerInfo.employeeID = employerInfo.employeeID;
            $scope.employerInfo.employeeName = employerInfo.employeeName;
            $scope.employerInfo.salary = employerInfo.salary;
            $scope.employerInfo.contactAddress = employerInfo.contactAddress;
            $scope.employerInfo.mobileNo = employerInfo.mobileNo;
            $scope.employerInfo.dob = employerInfo.dob;
            $scope.employerInfo.isActive = employerInfo.isActive;
            $scope.employerInfo.branchID = employerInfo.branchID;
        };

        $scope.updateConfirm = function (employerInfo) {
            $.ajax({
                type: 'PUT',
                contentType: 'application/json',
                url: '/Employer/PutEmployer',
                data: JSON.stringify(employerInfo),
                success: function (data) {
                    $scope.EmployerArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Delete
        $scope.deleteInformation = function (employerInfo) {
            var state = confirm("Do you want to delete data????");
            if (state === true) {
                $.ajax({
                    type: 'DELETE',
                    contentType: 'application/json',
                    url: '/Employer/DeleteEmployer',
                    data: JSON.stringify(employerInfo),
                    success: function (data) {
                        $scope.EmployerArray = data;
                        $scope.load();
                        $scope.clear();
                    }
                });
            }
        };

        $scope.cancel = function () {
            $scope.clear();
        };
    }
])