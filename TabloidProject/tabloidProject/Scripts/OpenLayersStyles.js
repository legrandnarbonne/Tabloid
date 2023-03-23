﻿var StyleLignes = new ol.style.Style({
    fill: new ol.style.Fill({
        color: [250, 250, 250, 0.1]
    }),
    stroke: new ol.style.Stroke({
        color: [220, 220, 220, 1],
        width: 1
    })
});


var StyleVectCommunes = new ol.style.Style({
    fill: new ol.style.Fill({
        color: [250, 250, 250, 0.1]
    }),
    stroke: new ol.style.Stroke({
        color: [220, 220, 220, 1],
        width: 1
    })
});

var StyleCache = new ol.style.Style({
    fill: new ol.style.Fill({
        color: [255, 255, 255, 0.5]
    }),
    stroke: new ol.style.Stroke({
        color: [255, 255, 255, 1],
        width: 1
    })
});

var StyleNomCommunes = function (feature, resolution) {
    return new ol.style.Style({
        text: new ol.style.Text({
            font: '12px helvetica,sans-serif',
            text: feature.get('n_com'),
            fill: new ol.style.Fill({
                color: '#000'
            }),
            stroke: new ol.style.Stroke({
                color: [255, 255, 255, 0.5],
                width: 2
            })
        })
    })
};

var objetStyle = function (feature, resolution) {
    if (feature.get('flux')) {
        var img = feature.get('flux').replace(/ /g, '_');
        return new ol.style.Style({
            image: new ol.style.Icon(({
                anchor: [0.5, 1],
                offset: [0, 0],
                size: [11, 15],
                scale: 1.5,
                anchorXUnits: 'fraction',
                anchorYUnits: 'fraction',
                src: '/images/flux/' + img + '.png'
            })),
            stroke: new ol.style.Stroke({
                color: 'green',
                width: 2
            })
        });
    }
};		

var objetStyle = function (feature, resolution) {
	var img = "l3";
	
    if (feature.get('a_fact')=='t') {
        var img = "l4";
	};
	
	if (feature.get('a_fact')=='t') {
        var img = "l5";
	};
	
	
    return new ol.style.Style({
            image: new ol.style.Icon(({
                anchor: [0.5, 1],
                offset: [0, 0],
                size: [20, 20],
                scale: 1,
                anchorXUnits: 'fraction',
                anchorYUnits: 'fraction',
                src: '/tabloid/images/' + img + '.png'
            })),
            stroke: new ol.style.Stroke({
                color: 'green',
                width: 2
            })
        });
    
};