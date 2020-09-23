var parcelAry = new Array();
var GISParcelAry = new Array();
var GISParcel, County, len = 0, X, Y;
var lat, lon;
var myMap, sr, infoSymbol, highlightSymbol,ex, myMapView;
var dialogBox, PointLayer, currentScale;
var pt, gsvc, inSR, defaultPopupTemplate;
var imageryRef, darkthemeRef, hybridRef, parcelLayer, parcelgLayer;
var geocoderURL;
var locator;
var street;
var hfIsChesapeake = '#hfIsChesapeake';
var hfIsLocked = "#hfIsLocked"; 
var hfTaxParcel1 = 'hfTaxParcel';
var hfTaxParcel = '#hfTaxParcel';
var txtLatitude = '#txtLatitude';
var txtLongitude = '#txtLongitude';
var hfSiteInfo = '#hfSiteInfo';
var mapDiv = 'mapDiv';
var hfX = '#hfX';
var hfY = '#hfY';
var txtWatershed = '#txtWatershed';
var hfWaterShedCode = '#hfWaterShedCode';


require([
"esri/Map",
"esri/Basemap",
"esri/views/MapView",
"esri/geometry/SpatialReference",
"esri/config",
"esri/tasks/Locator",
"esri/tasks/GeometryService",
"esri/geometry",
"esri/geometry/support/webMercatorUtils",
"esri/geometry/Point",
"esri/geometry/Extent",
"esri/symbols/PictureMarkerSymbol",
"esri/layers/MapImageLayer",
"esri/layers/ImageryLayer",
"esri/layers/GraphicsLayer",
"esri/layers/FeatureLayer",
"esri/layers/TileLayer",
"esri/layers/VectorTileLayer",
"esri/renderers/SimpleRenderer",
"esri/Graphic",
"esri/tasks/support/ProjectParameters",
"esri/tasks/support/Query",
"esri/tasks/QueryTask",
"esri/symbols/SimpleFillSymbol",
"esri/symbols/SimpleLineSymbol",
"esri/symbols/SimpleMarkerSymbol",
"esri/PopupTemplate",
"dojo/parser",
"dojo/dom"],
function (map,Basemap,MapView, SpatialReference, esriConfig, Locator, GeometryService, geometry,
            webMercatorUtils, Point, Extent, PictureMarkerSymbol, MapImageLayer, ImageryLayer, GraphicsLayer,FeatureLayer,TileLayer,VectorTileLayer,
            SimpleRenderer, Graphic, ProjectParameters,
            query, QueryTask, SimpleFillSymbol, SimpleLineSymbol, SimpleMarkerSymbol,PopupTemplate, parser, dom) {

    //parser.parse();

    var DarkThemeURL = "https://firstmap.delaware.gov/arcgis/rest/services/BaseMap/DE_DarkGreyCache/MapServer"
    var imageURL = "https://imagery.firstmap.delaware.gov/imagery/rest/services/DE_Imagery/DE_Imagery_2017/ImageServer"
    sr = new SpatialReference({ wkid: 26957 });   //state plane
    inSR = new SpatialReference({ wkid: 102100 }); // web mercator 
    ex = new Extent(-8718921.556524755, 4542902.3890665015, -8021815.858563982, 4909800.12483533, inSR);


    // define the template to show the outfall points
    defaultPopupTemplate = new PopupTemplate({
        title: "{name}",
        content: [{
            type: "fields",
            fieldInfos: [{
                fieldName: "lat",
                label: "Latitude",
            }, {
                fieldName: "lon",
                label: "Longitude",
            }]
        }]
    });

    highlightSymbol = {
        type: "simple-fill",  // autocasts as new SimpleFillSymbol()
        color: [223, 223, 0, 0.1], //[125, 125, 125, 0.35],
        style: "solid",
        outline: {  // autocasts as new SimpleLineSymbol()
            style: "solid",
            color: [223, 223, 0, 1], //[255, 0, 0],
            width: "3"
        }
    };

    infoSymbol = {
        color: [255, 0, 0],
        size: "12",
        type: "simple-marker",
        style: "circle",
        outline: {
            color: [0, 0, 0, 255],
            width: "1",
            type: "simple-line",
            style: "solid"
        }
    };

    exSymbol = {
        color: [127, 237, 167],
        size: "12",
        type: "simple-marker",
        style: "circle",
        outline: {
            color: [0, 0, 0, 255],
            width: "1",
            type: "simple-line",
            style: "solid"
        }
    };

    // when using the feature layer
    //var parcelRenderer = {
    //    type: "unique-value",
    //    field: "PIN",
    //    defaultSymbol: {
    //        type: "simple-line",
    //        color: "yellow", // [255, 255, 255, 0.5],
    //        style: "solid",
    //        width: 1
    //    }

    //};

    //labelClass = {
    //    labelExpressionInfo: { expression: "$feature.[PIN]" },
    //    labelPlacement: "always-horizontal",
    //    symbol: {
    //        type: "text",
    //        color: "orange",
    //        haloColor: "white",
    //        haloSize: 1,
    //        text: "you are here",
    //        font: {
    //            weight: "bold",
    //            family: "Arial",
    //            style: "normal",
    //        }
    //    }
    //};




    if (dom.byId(hfTaxParcel1) != null) {




        imageryRef = new ImageryLayer({ url: imageURL });
        darkthemeRef = new TileLayer({ url: DarkThemeURL });
        var bmap = new Basemap({
            baseLayers: [darkthemeRef, imageryRef],
            title: "satellite",
            id: "myBasemap"
        })


        myMap = new map({
            basemap: bmap

        });


        myMapView = new MapView({
            map: myMap,
            container: 'mapDiv',
            zoom: 4,
            popup: {
                dockEnabled: false,
                dockOptions: {
                    buttonEnabled: false,
                    breakpoint: false
                }
            },
            extent: ex,
            spatialReference:inSR
        });

        

        geocoderURL = "https://enterprise.firstmap.delaware.gov/arcgis/rest/services/Location/Delaware_FirstMap_Locator/GeocodeServer";
        gsvc = new GeometryService("https://enterprise.firstmap.delaware.gov/arcgis/rest/services/Utilities/Geometry/GeometryServer")



       
        if ($(hfTaxParcel).val().length > 0) {
            parcelAry = $(hfTaxParcel).val().split(",")
        }
        len = parcelAry.length;
        for (var i = 0; i < len; i++) {
            getGISParcel(parcelAry[i]);
        }


        X =$(hfX).val();
        Y = $(hfY).val();
        lat = $(txtLatitude).val();
        lon = $(txtLongitude).val();
        street = $(hfSiteInfo).val();



        hybridRef = new VectorTileLayer({
            url: "https://www.arcgis.com/sharing/rest/content/items/af6063d6906c4eb589dfe03819610660/resources/styles/root.json"
        });
        parcelLayer = new MapImageLayer({
            url: "https://enterprise.firstmap.delaware.gov/arcgis/rest/services/PlanningCadastre/DE_StateParcels/MapServer",
            id: "parcelLayer"//,
        });
        parcelgLayer = new GraphicsLayer({ id: "parcelgLayer" })
        PointLayer = new GraphicsLayer({ id: 'PointLayer' });

        myMap.addMany([hybridRef, parcelLayer, parcelgLayer, PointLayer]);

        myMapView.when(function () {
            currentScale = myMapView.scale
            //hightlightoutfalls();
        }, function (error) {
            myMapView.popup.open({
                title: "Error!",
                content: "The view's resources failed to load: " + error
            });
        });

        if ( $(hfIsLocked).val() == "N") {
            myMapView.on("click", function (evt) {
                var content;
                evt.stopPropagation();
                currentScale = myMapView.scale;
                if (currentScale > 1200) {
                    myMapView.popup.open({
                        content: "Please ZOOM IN to place a point!",
                        location: evt.mapPoint
                    });
                }
                else {

                   // var point = evt.mapPoint;
                   // mapPoint(point);
                    WaterShedSearch(evt)
                }
            });
        }
        
        myMapView.on("zoom-end", function () {
            currentScale = myMapView.scale;
        });


        if (parcelAry.length == 0) {
            if (street == undefined) {
                ZoomStatePlane(myMap);
            }
            else {
                locateAddress(street);
            }
        }
        else {
            parcelSearch()
           // WaterShedSearchByLatLong(lat, lon);
        }

        if (lat != undefined) {
            if (lat != "") {
                WaterShedSearchByLatLong(lat, lon);
            }
        }


    }
});


function getGISParcel(parcel) {
    //debugger;
    var tempParcel
    if (parcel.length == 13) {
        County = "NEWCASTLE"
        //tempParcel = parcel.replace(/[.]/g, '')
        //tempParcel = tempParcel.replace(/-/g, '')

        tempParcel = FormatNewCastleParcel(parcel)
    }
    if (parcel.length == 25) {
        County = "KENT"
        //tempParcel = parcel.substr(0, 2) + parcel.substr(3, 3) + parcel.substr(6, 15)

        //tempParcel = parcel.replace(/[.]/g, '')
        //tempParcel = tempParcel.replace(/-/g, '')
        //tempParcel = tempParcel.substr(0, tempParcel.length - 3)

        tempParcel = FormatKentParcel(parcel);
    }
    if (parcel.length == 18) {

        County = "SUSSEX"
        //tempParcel = parcel.replace(/-/g, '')
        //tempParcel = tempParcel.replace(/[.]/g, '')
        //tempParcel = tempParcel.substr(0, 3) + '0' + tempParcel.substr(3, 10)
        tempParcel = FormatSussexParcel(parcel)
    }
    GISParcelAry.push(tempParcel);
}

function parcelSearch() {
    //build query task
    require(["esri/tasks/support/Query", "esri/tasks/QueryTask",
        "dojo/on"], function (query, QueryTask, on) {


            var queryTask;
            var query = new query();
            var whereClause = '';
            var queryField;

            queryField = "PIN='"
            queryTask = new QueryTask("https://enterprise.firstmap.delaware.gov/arcgis/rest/services/PlanningCadastre/DE_StateParcels/MapServer/0")



            //Can listen for onComplete event to process results or can use the callback option in the queryTask.execute method.
            //dojo.connect(queryTask, "onComplete", mapResults);
            //on(queryTask, "onComplete", mapResults);

            query.returnGeometry = true;
            query.outFields = ["PIN"];
            if (len > 1) {
                for (var i = 0; i < len; i++) {
                    if (i < len - 1) {
                        whereClause = whereClause + queryField + GISParcelAry[i] + "' or ";
                    } else {
                        whereClause = whereClause + queryField + GISParcelAry[i] + "'";
                    }
                }
            } else {
                whereClause = queryField + GISParcelAry[0] + "'";
            }
            query.where = whereClause

            queryTask.execute(query).then(mapResults);
        });
}

function mapResults(featureSet) {
    require([
            "esri/geometry/SpatialReference",
            "esri/Graphic",
            "esri/geometry/Point",
            "esri/geometry/Extent","esri/tasks/support/ProjectParameters",
            "esri/geometry","esri/geometry/support/webMercatorUtils"], function (SpatialReference, Graphic, Point, Extent,ProjectParameters, geometry,webMercatorUtils) {


        if (featureSet.features.length <= 0) {
            //alert('Unable to locate parcel');

            if (X != "") {
                //alert('x available')
                var params = new ProjectParameters();
                var inputpoint = new Point(X, Y, sr);
                params.geometries = [inputpoint];
                params.outSpatialReference = inSR;

                gsvc.project(params).then(function (projectedPoints) {

                    var pt;
                    pt = projectedPoints[0];

                    minx = pt.x - 500;
                    maxx = pt.x + 500;
                    miny = pt.y - 500;
                    maxy = pt.y + 500;

                    var newExtent = new Extent({
                        xmin: minx,
                        ymin: miny,
                        xmax: maxx,
                        ymax: maxy,
                        spatialReference: inSR
                    });

                    var att = {
                        "name": "New",
                        "lat": pt.latitude,
                        "lon": pt.longitude
                    };

                    mapPoint(PointLayer, pt, att, infoSymbol, true);
                    //myMapView.extent = newExtent.expand(1);
                    //WaterShedSearchByLatLong(lat, lon);

                    WaterShedQuery(pt)
                });

            }
            else {
                PointLayer.removeAll;
                //myMapView.popup.close();
                ZoomStatePlane(myMapView)
            }

            myMapView.popup.open({
                title: "Warning!",
                content: "Unable to locate parcel",
                location: myMapView.center
            });
        }
        else {

            parcelgLayer.removeAll();
            //QueryTask returns a featureSet.  Loop through features in the featureSet and add them to the map.
            for (var i = 0, il = featureSet.features.length; i < il; i++) {
                //Get the current feature from the featureSet.
                //Feature is a graphic
                var graphic = featureSet.features[i];
                graphic.symbol = highlightSymbol;

                parcelgLayer.add(graphic);
                var graphicExtent = new Extent
                graphicExtent = graphic.geometry.extent
                myMapView.extent = graphicExtent.expand(1);
            }
        }
    });

}

function ZoomStatePlane(myMap) {
    require(["esri/geometry/Extent"], function (Extent) {

        var newExtent = new Extent(-8718921.556524755, 4542902.3890665015, -8021815.858563982, 4909800.12483533, inSR);
        myMapView.extent = newExtent.expand(1);

    });
}

function FormatKentParcel(parcel) {
    var taxkenthundred = { "DC": "1", "ED": "2", "KH": "3", "LC": "4", "MD": "5", "MN": "6", "NM": "7", "SM": "8", "WD": "9" }
    var tempparcel;
    //debugger;
    tempparcel = taxkenthundred[parcel.substr(0, 2)] + parcel.substr(2, 19).replace(/[.]/g, '') + '-' + parcel.substr(22, 3) + '01';
    return tempparcel;
}

function FormatNewCastleParcel(parcel) {
    var tempparcel;
    tempparcel = parcel.replace(/[.]/g, '')
    tempparcel = tempparcel.replace(/-/g, '')
    return tempparcel
}

function FormatSussexParcel(parcel) {
    var tempparcel;
    tempparcel = parcel.substr(0, 4).replace(/[-]/g, '') + parcel.substr(4, 1) + Number(parcel.substr(5, 2)).toString() + parcel.substr(7, 4) + Number(parcel.substr(11, 4)).toString() + parcel.substr(15, 3);
    return tempparcel;
}

function mapPoint(layer, pt, att, sym, reset) {
    require(["esri/Graphic", "esri/symbols/SimpleMarkerSymbol"], function (Graphic, SimpleMarkerSymbol) {


        if (reset) {
            layer.removeAll();
        }

        layer.graphics.add(new Graphic({ geometry: pt, symbol: sym, attributes: att, popupTemplate: defaultPopupTemplate }));

        myMapView.on("pointer-move", function (event) {
            myMapView.hitTest(event).then(function (response) {
                if (response.results.length) {
                    var graphic = response.results.filter(function (result) {
                        return result.graphic.layer === PointLayer;
                    })[0].graphic;
                    var attributes = graphic.attributes;
                    myMapView.popup.open({
                        location: response.results[0].mapPoint,
                        title: attributes.name,
                        content: "<table class='esri-widget__table'><tbody><tr><th>Latitude</th><td>" + attributes.lat + "</td></tr><tr><th>Longitude</th><td>" + attributes.lon + "</td></tr></tbody></table>"
                        //</div>"Latitude = " + attributes.lat + "<br/> Longitude = " + attributes.lon,
                    });
                }
            })
        });
    });
}

function locateAddress(street) {
    require(["esri/tasks/Locator"], function (Locator) {

        locator = new Locator("https://enterprise.firstmap.delaware.gov/arcgis/rest/services/Location/Delaware_FirstMap_Locator/GeocodeServer/findAddressCandidates?");

        var address1 = {
            "SingleLine": street //,
            //Zone: zip
        };
        var params = { address: address1 };
        locator.outSpatialReference = myMapView.extent.spatialReference;
        if (street != "") {
            locator.addressToLocations(params).then(showResults);
        }
    });

}

function showResults(candidates) {
    require(["esri/geometry"], function (geometry) {

        var candidate;
        var minX, miny, maxx, maxy
        //var symbol = new esri.symbol.SimpleMarkerSymbol();
        // var infoTemplate = new esri.InfoTemplate("Location", "Address: ${address}<br />Score: ${score}<br />Source locator: ${locatorName}");

        //symbol.setStyle(esri.symbol.SimpleMarkerSymbol.STYLE_DIAMOND);
        //symbol.setColor(new dojo.Color([255, 0, 0, 0.75]));

        var newExtent;
        var points = new geometry.Multipoint(myMapView.extent.spatialReference);

        for (var i = 0, il = candidates.length; i < il; i++) {
            candidate = candidates[i];

            if (candidate.score > 80) {

                //var attributes = { address: candidate.address, score: candidate.score, locatorName: candidate.attributes.Loc_name };
                //var graphic = new Graphic(candidate.location, symbol, attributes, infoTemplate);
                //myMap.graphics.add(graphic);
                //myMap.graphics.add(new esri.Graphic(candidate.location, new esri.symbol.TextSymbol(attributes.address).setOffset(0, 8)));
                points.addPoint(candidate.location);

                minx = candidate.location.x - 100;
                maxx = candidate.location.x + 100;
                miny = candidate.location.y - 100;
                maxy = candidate.location.y + 100;

                newExtent = new geometry.Extent();
                newExtent.xmin = minx;
                newExtent.ymin = miny;
                newExtent.xmax = maxx;
                newExtent.ymax = maxy;
                newExtent.spatialReference = inSR

                //myMap.setExtent(points.getExtent().expand(1));
            }
        }
        if (candidates.length < 1) {

            alert("Sorry, no match found for address. Please verify the address and try again.");
        }
        if (points.points.length === 0) {

            alert("Sorry, no match found for address. Please verify the address and try again.");
        }
        myMapView.extent = newExtent;
    });
}

function WaterShedSearch(evt) {

    require(["esri/geometry", "esri/geometry/support/webMercatorUtils", "esri/tasks/support/ProjectParameters"], function (geometry, webMercatorUtils, ProjectParameters) {

        var latlong = webMercatorUtils.webMercatorToGeographic(evt.mapPoint)
        $(txtLatitude).val(parseFloat(latlong.y.toFixed(6)));
        $(txtLongitude).val(parseFloat(latlong.x.toFixed(6)));


        var att = {
            "name": "New",
            "lat": latlong.y,
            "lon": latlong.x
        };

        mapPoint(PointLayer, evt.mapPoint, att, infoSymbol, true)

        WaterShedQuery(evt.mapPoint);
        //IsChesapeakeQuery(evt.mapPoint); could use HUC6 from HUC12 to filter the isChesapeake


        /*Start converting the x and y to delaware state plane*/

        var params = new ProjectParameters();
        var inputpoint = new geometry.Point(evt.mapPoint.x, evt.mapPoint.y, inSR);
        params.geometries = [evt.mapPoint];
        params.outSpatialReference = sr; // DE State Plan

        gsvc.project(params).then(function (projectedPoints) {
            pt = projectedPoints[0];
            $(hfX).val(pt.x);
            $(hfY).val(pt.y);
        });

        /*End converting the x and y to delaware state plane*/

    });
}

function WaterShedQuery(point) {
    require(["esri/tasks/support/Query", "esri/tasks/QueryTask"], function (Query, QueryTask) {
        var queryTask1;
        var query1 = new Query();
        queryTask1 = new QueryTask("https://enterprise.firstmap.delaware.gov/arcgis/rest/services/Hydrology/DE_Watersheds/MapServer/5")
        query1.returnGeometry = true;
        query1.outFields = ["*"];
        query1.geometry = point;
        queryTask1.execute(query1).then(WaterShedResults);
    });
}

function WaterShedResults(featureSet) {


    if (featureSet.features.length > 0) {

        $(txtWatershed).val(featureSet.features[0].attributes["NAME"]);
        $(hfWaterShedCode).val(featureSet.features[0].attributes["HUC12"]);

    }
    else {
        
        myMapView.popup.open({
            title: "Warning!",
            content: "Unable to locate the Watershed.",
            location: myMapView.center
        });

    }
}

function IsChesapeakeQuery(point) {
    require(["esri/tasks/support/Query", "esri/tasks/QueryTask"], function (Query, QueryTask) {
        var queryTask1;
        var query1 = new Query();
        queryTask1 = new QueryTask("https://enterprise.firstmap.delaware.gov/arcgis/rest/services/Hydrology/DE_Watersheds/MapServer/2")
        query1.returnGeometry = true;
        query1.outFields = ["*"];
        query1.geometry = point;
        queryTask1.execute(query1).then(IsChesapeakeResults);
    });
}

function IsChesapeakeResults(featureSet) {


    if (featureSet.features.length > 0) {

        if (featureSet.features[0].attributes["NAME"] == 'Lower Chesapeake' || featureSet.features[0].attributes["NAME"] == 'Upper Chesapeake') {
            $(hfIsChesapeake).val('Y');
        }
        else {
            $(hfIsChesapeake).val('N');
        }
    }
    //else {

    //    myMapView.popup.open({
    //        title: "Warning!",
    //        content: "Unable to locate the Watershed.",
    //        location: myMapView.center
    //    });

    //}
}

function WaterShedSearchByLatLong(lat, lon) {
    require(["esri/geometry", "esri/tasks/support/ProjectParameters", "esri/geometry/Point", "esri/geometry/support/webMercatorUtils"], function (geometry, ProjectParameters, Point,webMercatorUtils) {

        var minX, miny, maxx, maxy;

        //sr = new esri.SpatialReference({ wkid: 26957 });
        //inSR = new esri.SpatialReference({ wkid: 102100 });

        //gsvc = new esri.tasks.GeometryService("https://firstmap.gis.delaware.gov/arcgis/rest/services/Utilities/Geometry/GeometryServer")


        //lat = $(txtLatitude).val();
        //lon = $(txtLongitude).val();
        var x, y;

        var latlongxy = webMercatorUtils.lngLatToXY(lon, lat)
        
        var inputpoint = new Point(latlongxy[0], latlongxy[1], inSR);



        var att = {
            "name": "New",
            "lat": lat,
            "lon": lon
        };

        mapPoint(PointLayer, inputpoint, att, infoSymbol, true);


        minx = inputpoint.x - 100;
        maxx = inputpoint.x + 100;
        miny = inputpoint.y - 100;
        maxy = inputpoint.y + 100;

        newExtent = new geometry.Extent();
        newExtent.xmin = minx;
        newExtent.ymin = miny;
        newExtent.xmax = maxx;
        newExtent.ymax = maxy;
        newExtent.spatialReference = inSR
        myMapView.extent = newExtent;                
        if (lat.length != 0 || lon.length != 0) {
            WaterShedQuery(inputpoint);
            IsChesapeakeQuery(inputpoint);
        }

        var params = new ProjectParameters();
        params.geometries = [inputpoint];
        //params.inSR = { wkid: 102100 }; // WebMercator
        params.outSpatialReference = sr; // DE State Plan


        gsvc.project(params).then(function (projectedPoints) {

            pt = projectedPoints[0];
            $(hfX).val(pt.x);
            $(hfY).val(pt.y);

        });

    });

}