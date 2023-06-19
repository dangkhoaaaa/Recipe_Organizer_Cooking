/*
Template Name: Admin Pro Admin
Author: Wrappixel
Email: niravjoshi87@gmail.com
File: js
*/
$(function() {
    "use strict";
    // ============================================================== 
    // Our Visitor
    // ============================================================== 

    var chart = c3.generate({
        bindto: '#visitor',
        data: {
            columns: [
                ['Desktop', 60],
                ['Tablet', 12],
                ['Mobile', 28],

            ],

            type: 'donut',
            onclick: function(d, i) { console.log("onclick", d, i); },
            onmouseover: function(d, i) { console.log("onmouseover", d, i); },
            onmouseout: function(d, i) { console.log("onmouseout", d, i); }
        },
        donut: {
            label: {
                show: false
            },
            title: "Order",
            width: 35,

        },

        legend: {
            hide: true
            //or hide: 'data1'
            //or hide: ['data1', 'data2']
        },
        color: {
            pattern: ['#40c4ff', '#2961ff', '#ff821c', '#7e74fb']
        }
    });

    // ============================================================== 
    // Conversation Rate
    // ============================================================== 
    var chart = c3.generate({
        bindto: '.rate',
        data: {
            columns: [
                ['Conversation', 85],
                ['other', 15],
            ],
            
            type : 'donut',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        donut: {
            label: {
                show: false
              },
            title:"",
            width:5,
            
        }
        , padding: {
            top:10,
             bottom:-20
            
        , },
        legend: {
          hide: true
          //or hide: 'data1'
          //or hide: ['data1', 'data2']
        },
        color: {
              pattern: ['#fff', '#81c784', '#fff', '#7e74fb']
        }
    });

    // ============================================================== 
    // product-sales
    // ============================================================== 
    var chart = c3.generate({
        bindto: '.product-earning'
        , data: {
            columns: [
                ['Site A', 5, 6, 3, 7, 9, 10, 14, 12]
                , ['Site B', 1, 2, 8, 3, 4, 5, 7, 6]
            ]
            , type: 'bar'
        }
        , axis: {
            y: {
                show: false
                , tick: {
                    count: 0
                    , outer: false
                }
            }
            , x: {
                show: false
            , }
        },bar: {
          
          width: 8
            
        }
        , padding: {
            top: 0
            , right: 0
            , bottom: -28
            , left: 0
        , }
        , point: {
            r: 0
        , }
        , legend: {
            hide: true
                //or hide: 'data1'
                //or hide: ['data1', 'data2']
        }
        , color: {
            pattern: ['#fff', '#40c4ff', '#fff', '#7e74fb']
        }
    });

    // ============================================================== 
    // Monthly Overview
    // ==============================================================
    Morris.Area({
        element: 'profit',
        data: [{
                    period: '2010',
                    pro: 0,
                }, {
                    period: '2011',
                    pro: 150,
                }, {
                    period: '2012',
                    pro: 80,
                }, {
                    period: '2013',
                    pro: 100,
                }, {
                    period: '2014',
                    pro: 80,
                }, {
                    period: '2015',
                    pro: 125,
                }, {
                    period: '2016',
                    pro: 130,
                }, {
                    period: '2017',
                    pro: 180,
                }, {
                    period: '2018',
                    pro: 135,
                }


                ],
                lineColors: ['#6dcffe'],
                xkey: 'period',
                ykeys: ['pro'],
                labels: ['Profit'],
                pointSize: 0,
                lineWidth: 0,
                resize:true,
                fillOpacity: 1,
                behaveLikeLine: true,
                gridLineColor: '#e0e0e0',
                hideHover: 'auto'
        
    });
});