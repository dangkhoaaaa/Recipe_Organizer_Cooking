/*
Template Name: Material Admin
Author: Wrappixel
Email: niravjoshi87@gmail.com
File: js
*/
$(function() {
    "use strict";
    // ============================================================== 
    // Last month earning
    // ==============================================================
    /*var sparklineLogin = function() {
        $('.crypto').sparkline([6, 10, 9, 11, 9, 10, 12], {
            type: 'bar',
            height: '30',
            barWidth: '4',
            width: '100%',
            resize: true,
            barSpacing: '5',
            barColor: '#ffffff'
        });

    };
    var sparkResize;

    $(window).resize(function(e) {
        clearTimeout(sparkResize);
        sparkResize = setTimeout(sparklineLogin, 500);
    });
    sparklineLogin();*/

    // ============================================================== 
    // BitCoin / Ethereum
    // ==============================================================
    var chart = c3.generate({
        bindto: '.btc-rpl',
        data: {
            columns: [
                ['Site A', 5, 6, 3, 7, 9, 10, 14, 12, 11, 9, 8, 7, 10, 6, 12, 10, 8],
                ['Site B', 1, 2, 8, 3, 4, 5, 7, 6, 5, 6, 4, 3, 3, 12, 5, 6, 3]
            ],
            type: 'line'
        },
        axis: {
            y: {
                show: true,
                tick: {
                    count: 0,
                    outer: false
                }
            },
            x: {
                show: true,
            }
        },
        padding: {
            top: 40,
            right: 10,
            bottom: 40,
            left: 20,
        },
        point: {
            r: 4,
        },
        legend: {
            hide: true
                //or hide: 'data1'
                //or hide: ['data1', 'data2']
        },
        color: {
            pattern: ['#2961ff', '#ff9800']
        }
    });

});