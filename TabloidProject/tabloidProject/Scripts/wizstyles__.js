var wizStyles={
    "styleemprise": {
        "fill": { "color": [100, 0, 46, 0.2] },  "geometry": function (feature) {			
            var coordinates = feature.getGeometry().getCoordinates()[0][0];
            return new ol.geom.Circle(coordinates, 100);
        }
    }
};