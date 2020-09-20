var chart = $('#lineChart');
var chartData = [];


$(document).ready(function () {
    loadData();
});


function loadData() {
    $.ajax({
        url: "/Dashboards/LoadChartData",
        data: "",
        cache: false,
        type: "GET",
        dataType: "JSON"
    }).then((result) => {
        if (result.Item1.StatusCode == 200) {
            chartData = result.Item2;
            update();
        }
    });
}

function update() {
    myChart.data.datasets[0].data = chartData;
    myChart.update();
}

var myChart = new Chart(chart, {
    type: 'line',
    data: {
        labels: ['Senin', 'Selasa', 'Rabu', 'Kamis', 'Jumat'],
        datasets: [{
            label: 'Jumlah Terindikasi',
            data: chartData,
            borderColor: "#c45850",
        }],
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
});