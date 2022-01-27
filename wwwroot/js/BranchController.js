/// <reference path="../lib/angular.js/angular.js" />
/// <reference path="../lib/angular-route/angular-route.js" />

angular.module("BranchApp", []).controller("BranchCtrl", ['$scope',
    function ($scope) {
        $scope.BranchArray = [];
        $scope.load;
        $scope.load = function () {
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

        $scope.load();
        $scope.branchInfo = { branchID: '', branchNaname: '', branchLocation: '', division: '' };
        $scope.clear = function () {
            $scope.branchInfo.branchID = '';
            $scope.branchInfo.branchName = '';
            $scope.branchInfo.branchLocation = '';
            $scope.branchInfo.division = '';
        };

        //Add
        $scope.branchInsert = function (branchInfo) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json',
                url: '/Branch/AddBranch',
                data: JSON.stringify(branchInfo),
                success: function (data) {
                    $scope.BranchArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Update
        $scope.updateStart = function (branchInfo) {
            $scope.branchInfo.branchID = branchInfo.branchID;
            $scope.branchInfo.branchName = branchInfo.branchName;
            $scope.branchInfo.branchLocation = branchInfo.branchLocation;
            $scope.branchInfo.division = branchInfo.division;
        };

        $scope.updateConfirm = function (branchInfo) {
            $.ajax({
                type: 'PUT',
                contentType: 'application/json',
                url: '/Branch/UpdateBranch',
                data: JSON.stringify(branchInfo),
                success: function (data) {
                    $scope.BranchArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Delete
        $scope.deleteInformation = function (branchInfo) {
            var state = confirm("Do you want to delete data????");
            if (state === true) {
                $.ajax({
                    type: 'DELETE',
                    contentType: 'application/json',
                    url: '/Branch/DeleteBranch',
                    data: JSON.stringify(branchInfo),
                    success: function (data) {
                        $scope.BranchArray = data;
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