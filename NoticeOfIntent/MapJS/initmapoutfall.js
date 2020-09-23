var parcelAry = new Array();
var GISParcelAry = new Array();
var GISParcel, County, len = 0, X, Y;
var myMap, sr, infoSymbol,exSymbol,highlightSymbol, ex, myMapView;
var dialogBox, PointLayer, currentScale;
var pt, gsvc, inSR, street;
var OutfallTemplate, oldOutfallTemplate;
var imageryRef,darkthemeRef,hybridRef, parcelLayer,parcelgLayer,newOutfallLayer, outfallsLayer
var hfSiteInfo = '#hfSiteInfo';
var txtoutfallWatershed = '#txtoutfallWatershed';
var hfoutfallWaterShedCode = '#hfoutfallWaterShedCode';
var hfoutfallX = '#hfoutfallX';
var hfoutfallY = '#hfoutfallY';
var txtoutfallLatitude = '#txtoutfallLatitude';
var txtoutfallLongitude = '#txtoutfallLongitude';
var hfTaxParcel1 = 'hfTaxParcel';
var hfTaxParcel = '#hfTaxParcel';
var hfOutfalllist = '#hfOutfalllist'

function showMapInDialog() {

    //require([
    //        "dijit/Dialog",
    //        "dijit/TooltipDialog"], function (Dialog,TooltipDialog) {


    //            var geom;
    //            var point;

    //            if (!dialogBox) {



    //                var htmlFragment = '<div style="width:600px;"><h3>Click on the map to select the discharge point and close the window to save.<h3></div>';
    //                htmlFragment += '<div id="mapOne" style="width:600px; height:600px; border: 1px solid #A8A8A8;"></div>';

    //                //var htmlFragment ='<div class="modal fade" id="MapModal" tabindex="-1" role="dialog"><div class="modal-dialog" role="document"><div class="modal-content"><div class="modal-header">';
    //                //htmlFragment += '<h5 class="modal-title" id="noiModalLabel">Outfalls</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button></div><div class="modal-body" id="mapOne">';
    //                //htmlFragment += '</div><div class="modal-footer"><button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button><button type="button" class="btn btn-primary">Save changes</button></div></div></div></div>';




    //                // CREATE DIALOG
    //                dialogBox = new Dialog({
    //                    title: "Outfalls",
    //                    content: htmlFragment,
    //                    autofocus: !dojo.isIE, // NOTE: turning focus ON in IE causes errors when reopening the dialog
    //                    refocus: !dojo.isIE
    //                });


    //                // DISPLAY DIALOG
    //                dialogBox.show();

    //                // CREATE MAP
    //                createMap("mapOne");
    //                //dialogBox.refresh();
    //            }
    //            else {

    //                dialogBox.show();
    //                //highlightexistingoutfalls
    //            }
    
    //});

    $('#MapModal').modal("show");
}

function createMap(srcNodeRef) {

    require([
    "esri/Map",
    "esri/Basemap",
    "esri/views/MapView",
    "esri/geometry/SpatialReference",
    "esri/tasks/GeometryService",
    "esri/layers/GraphicsLayer",
    "esri/layers/ImageryLayer",
    "esri/layers/FeatureLayer",
    "esri/layers/MapImageLayer",
    "esri/layers/TileLayer",
    "esri/layers/VectorTileLayer",
    "esri/config",
    "esri/geometry/support/webMercatorUtils",
    "esri/PopupTemplate",
    "esri/geometry/Extent",
    "esri/renderers/SimpleRenderer",
    "esri/symbols/SimpleFillSymbol",
    "esri/symbols/SimpleLineSymbol",
    "esri/symbols/SimpleMarkerSymbol",
    "dojo/on"], function (map, Basemap, MapView, SpatialReference, GeometryService, GraphicsLayer, ImageryLayer, FeatureLayer, MapImageLayer, TileLayer, VectorTileLayer, esriConfig, webMercatorUtils, PopupTemplate, Extent, SimpleRenderer,
                            SimpleFillSymbol, SimpleLineSymbol,SimpleMarkerSymbol, on) {

        var DarkThemeURL = "https://firstmap.delaware.gov/arcgis/rest/services/BaseMap/DE_DarkGreyCache/MapServer"
            var imageURL = "https://imagery.firstmap.delaware.gov/imagery/rest/services/DE_Imagery/DE_Imagery_2017/ImageServer"

        

        // More Esri Maps to Try        
        hybridRef = new VectorTileLayer({
            url: "https://www.arcgis.com/sharing/rest/content/items/af6063d6906c4eb589dfe03819610660/resources/styles/root.json"
        });

        imageryRef = new ImageryLayer({ url: imageURL });
        darkthemeRef = new TileLayer({ url: DarkThemeURL });
        var bmap = new Basemap({
            baseLayers: [darkthemeRef, imageryRef],
            title: "satellite",
            id:"OutfallBasemap"
        })
        

        myMap = new map({
            basemap: bmap

        });

        
        // myMap = new esri.Map(srcNodeRef) //, {
        //extent: new esri.geometry.Extent(-19384354.257963974, -12688852.605287973, 19751404.224035975, 18619754.180311985, new esri.SpatialReference({ wkid: 102100 })),
        //slider: false
        //});

        myMapView = new MapView({
            map: myMap,
            container: srcNodeRef,
            zoom: 4,
            popup: {
                dockEnabled: false,
                dockOptions: {
                    buttonEnabled: false,
                    breakpoint:false
                }
            }
        });




        // define the template to show the outfall points
        OutfallTemplate = new PopupTemplate({
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

        sr = new SpatialReference({ wkid: 26957 });   // DE State plane
        inSR = new SpatialReference({ wkid: 102100 }); // WebMercator
        myMapView.extent = new Extent(-8718921.556524755, 4542902.3890665015, -8021815.858563982, 4909800.12483533, inSR);

        
        if ($(hfTaxParcel).val().length>0)
        {
            parcelAry = $(hfTaxParcel).val().split(",")
        }
        len = parcelAry.length;
        for (var i = 0; i < len; i++) {
            getGISParcel1(parcelAry[i]);
        }

        // ADD LAYER

        var parcelRenderer = {
            type: "unique-value",
            field: "PIN",
            defaultSymbol: {
                    type: "simple-line",
                    color: "yellow", // [255, 255, 255, 0.5],
                    style: "solid",
                    width: 1
            }

        };

        labelClass = {
            labelExpressionInfo: { expression: "$feature.[PIN]" },
            labelPlacement: "always-horizontal",
            symbol: {
                type: "text",
                color: "orange",
                haloColor: "white",
                haloSize: 1,
                text: "you are here",
                font: {
                    weight: "bold",
                    family: "Arial",
                    style: "normal",
                }
            }
        };




        parcelLayer = new MapImageLayer({
            url: "https://enterprise.firstmap.delaware.gov/arcgis/rest/services/PlanningCadastre/DE_StateParcels/MapServer",
            id: "parcelLayer"//,
        });
        parcelgLayer= new GraphicsLayer({id: "parcelgLayer"})
        newOutfallLayer = new GraphicsLayer({ id: "newOutfallLayer" });
        outfallsLayer = new GraphicsLayer({ id: "OutfallsLayer" });


        myMap.layers.add(hybridRef);
        myMap.layers.add(parcelLayer);
        myMap.layers.add(parcelgLayer);
        myMap.layers.add(outfallsLayer);
        myMap.layers.add(newOutfallLayer);


       // });


            gsvc = new GeometryService("https://enterprise.firstmap.delaware.gov/arcgis/rest/services/Utilities/Geometry/GeometryServer")

        //myMap.on("load", function () {
        //    currentScale = myMap.scale;
        //    hightlightoutfalls();
        //});

        myMapView.when(function () {
            currentScale=myMapView.scale
            hightlightoutfalls();
        }, function (error) {
            myMapView.popup.open({
                title: "Error!",
                content: "The view's resources failed to load: " + error
            });
        });


        if ($(hfIsLocked).val() == "N") {
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

                    WaterShedSearch1(evt)
                }
            });
        }


        myMapView.on("zoom-end", function () {
            currentScale = myMapView.getScale();
        });

        var lat, lon

        //lat = document.getElementById("txtLatitude").value
        // lon = document.getElementById("txtLongitude").value

        street = $(hfSiteInfo).val();

        if ((parcelAry.length == 0)){
            //&& (lat.length == 0 || lon.length == 0)) {
            if (street == undefined) {
                ZoomStatePlane1(myMapView)
            }
            else {
                locateAddress1(street);
            }
        }
        else {

            /*Start converting the x and y to WebMercator used to show the existing X and Y on the map if the Taxparcel is not found.*/
            //X = document.getElementById("hfX").value
            //Y = document.getElementById("hfY").value

            //var params1 = new ProjectParameters();
            //var inputpoint = new Point(X, Y, sr);
            //params1.geometries = [inputpoint];
            //params1.outSR = inSR;


            //gsvc.project(params1, function (projectedPoints) {
            //    pt = projectedPoints[0];
            //    X = pt.x
            //    Y = pt.y
            //});

            /*End converting the x and y to WebMercator*/


            // search for and zoom to the parcel on the load event.
            //dojo.connect(parcelLayer, "onLoad", parcelSearch)
            //on(parcelLayer,"onLoad",parcelSearch)

            parcelSearch1()

        }







        // var markerSymbol = new esri.symbol.SimpleMarkerSymbol(esri.symbol.SimpleMarkerSymbol.STYLE_X, 12, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0, 0.75]), 4));

        //if (document.getElementById("hfIsLocked").value == "N") {
        //dojo.connect(myMapView, "onClick", WaterShedSearch);
        //on(myMapView, "onClick", WaterShedSearch);
        //}

        //return myMapView;


    });

}

function parcelSearch1() {
    //build query task

    require(["esri/tasks/support/Query", "esri/tasks/QueryTask",
    "dojo/on"], function (query,QueryTask,on) {

        var queryTask;
        var query = new query();
        var whereClause = '';
        var queryField;

        queryField = "PIN='"
            queryTask = new QueryTask("https://enterprise.firstmap.delaware.gov/arcgis/rest/services/PlanningCadastre/DE_StateParcels/MapServer/0")



        //Can listen for onComplete event to process results or can use the callback option in the queryTask.execute method.
        //dojo.connect(queryTask, "onComplete", mapResults);
        //on(queryTask,"Complete",mapResults);

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

        queryTask.execute(query).then(mapResults1);
    });

}

function mapResults1(featureSet) {

    require([
        "esri/geometry/SpatialReference",
        "esri/Graphic",
        "esri/geometry/Point",
        "esri/geometry/Extent",
        "esri/geometry"], function (SpatialReference, Graphic, Point, Extent, geometry) {



            //var highlightSymbol = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 3), new dojo.Color([125, 125, 125, 0.35]));

            //var geom
            //var symbol
            //var point
            //var minX, miny, maxx, maxy

            if (featureSet.features.length <= 0) {
                //alert('Unable to locate parcel');
                var centerpoint = myMapView.center.clone();

                myMapView.popup.open({
                    title: "Warning!",
                    content: "Unable to locate parcel",
                    location: centerpoint
                });
                ZoomStatePlane1(myMapView)
            }
            else {

                parcelgLayer.removeAll();
                //QueryTask returns a featureSet.  Loop through features in the featureSet and add them to the map.
                for (var i = 0, il = featureSet.features.length; i < il; i++) {
                    //Get the current feature from the featureSet.
                    //Feature is a graphic
                    var graphic = featureSet.features[i];
                    graphic.symbol = highlightSymbol;


                    //Add graphic to the map graphics layer.
                    //myMap.graphics.add(graphic);

                    parcelgLayer.add(graphic);
                    var graphicExtent = new Extent
                    graphicExtent = graphic.geometry.extent
                    myMapView.extent=graphicExtent.expand(1);
                }
            }
        });
}

function ZoomStatePlane1(myMap) {
    requestAnimationFrame(["esri/geometry/Extent"], function (Extent) {

        var newExtent = new Extent(-8718921.556524755, 4542902.3890665015, -8021815.858563982, 4909800.12483533, inSR);
        myMapView.extent=newExtent.expand(1);

    });
}

//add existing outfall points
function hightlightoutfalls()
{
    require(["esri/geometry", "esri/geometry/support/webMercatorUtils"], function (geometry, webMercatorUtils) {

        var outfallsAry = new Array();
        if ($(hfOutfalllist).val().length>0){
            outfallsAry = $(hfOutfalllist).val().split("::");
        

            len = outfallsAry.length;

            for (var i = 0; i < len; i++) {
                var outfallinfo = outfallsAry[i].split(":");
                var latlongxy = webMercatorUtils.lngLatToXY(outfallinfo[2], outfallinfo[1])
                var outfall = new geometry.Point(latlongxy[0], latlongxy[1], inSR);
                var att = {
                    "name": outfallinfo[0],
                    "lat": outfallinfo[2],
                    "lon": outfallinfo[1]
                };
                mapPoint1(outfallsLayer, outfall, att, exSymbol, false);

            }
        }
    });
}

// Adds the new  point
function mapPoint1(layer,pt,att,sym,reset) {
    require(["esri/Graphic", "esri/symbols/SimpleMarkerSymbol"], function (Graphic, SimpleMarkerSymbol) {

        
        if (reset)
        {
            layer.removeAll();
        }
        
        layer.graphics.add(new Graphic({ geometry: pt, symbol: sym, attributes:att, popupTemplate:OutfallTemplate }));

        myMapView.on("pointer-move", function (event) {
            myMapView.hitTest(event).then(function (response) {
                if (response.results.length) {
                    var graphic = response.results.filter(function (result) {
                        return result.graphic.layer === newOutfallLayer || result.graphic.layer === outfallsLayer;
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

function getGISParcel1(parcel) {
    //debugger;
    var tempParcel
    if (parcel.length == 13) {
        County = "NEWCASTLE"
        //tempParcel = parcel.replace(/[.]/g, '')
        //tempParcel = tempParcel.replace(/-/g, '')

        tempParcel = FormatNewCastleParcel1(parcel)
    }
    if (parcel.length == 25) {
        County = "KENT"
        //tempParcel = parcel.substr(0, 2) + parcel.substr(3, 3) + parcel.substr(6, 15)

        //tempParcel = parcel.replace(/[.]/g, '')
        //tempParcel = tempParcel.replace(/-/g, '')
        //tempParcel = tempParcel.substr(0, tempParcel.length - 3)

        tempParcel = FormatKentParcel1(parcel);
    }
    if (parcel.length == 18) {

        County = "SUSSEX"
        //tempParcel = parcel.replace(/-/g, '')
        //tempParcel = tempParcel.replace(/[.]/g, '')
        //tempParcel = tempParcel.substr(0, 3) + '0' + tempParcel.substr(3, 10)
        tempParcel = FormatSussexParcel1(parcel)
    }
    GISParcelAry.push(tempParcel);
}

function FormatKentParcel1(parcel) {
    var taxkenthundred = { "DC": "1", "ED": "2", "KH": "3", "LC": "4", "MD": "5", "MN": "6", "NM": "7", "SM": "8", "WD": "9" }
    var tempparcel;
    //debugger;
    tempparcel = taxkenthundred[parcel.substr(0, 2)] + parcel.substr(2, 19).replace(/[.]/g, '') + '-' + parcel.substr(22, 3) + '01';
    return tempparcel;
}

function FormatNewCastleParcel1(parcel) {
    var tempparcel;
    tempparcel = parcel.replace(/[.]/g, '')
    tempparcel = tempparcel.replace(/-/g, '')
    return tempparcel
}

function FormatSussexParcel1(parcel) {
    var tempparcel;
    tempparcel = parcel.substr(0, 4).replace(/[-]/g, '') + parcel.substr(4, 1) + Number(parcel.substr(5, 2)).toString() + parcel.substr(7, 4) + Number(parcel.substr(11, 4)).toString() + parcel.substr(15, 3);
    return tempparcel;
}

function locateAddress1(st) {
    require(["esri/tasks/Locator"], function (Locator) {

        locator = new Locator("https://enterprise.firstmap.delaware.gov/arcgis/rest/services/Location/Delaware_FirstMap_Locator/GeocodeServer/findAddressCandidates?");  //https://firstmap.delaware.gov/arcgis/rest/services/Location/DE_CompositeLocator/GeocodeServer/findAddressCandidates?

        //street = $('#hfSiteInfo').val();


        var address = {
            "SingleLine": st //,
            //"f":"pjson"
        };
        var params = { address: address };
        locator.outSpatialReference = myMapView.extent.spatialReference;
        locator.addressToLocations(params).then(showResults1);
    });

}

function showResults1(candidates) {
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

                //myMapView.setExtent(points.getExtent().expand(1));
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


function WaterShedSearch1(evt) {
    require(["esri/geometry", "esri/geometry/support/webMercatorUtils", "esri/tasks/support/ProjectParameters"], function (geometry, webMercatorUtils, ProjectParameters) {




        var latlong = webMercatorUtils.webMercatorToGeographic(evt.mapPoint)
        $(txtoutfallLatitude).val(parseFloat(latlong.y.toFixed(6)));
        $(txtoutfallLongitude).val(parseFloat(latlong.x.toFixed(6)));


        /*Start converting the x and y to delaware state plane*/

        var params = new ProjectParameters();
        var inputpoint = new geometry.Point(evt.mapPoint.x, evt.mapPoint.y, inSR);
        params.geometries = [evt.mapPoint];
        //params.inSR = { wkid: 102100 }; // WebMercator
        params.outSpatialReference = sr; // DE State Plan


        gsvc.project(params).then(function (projectedPoints) {
            pt = projectedPoints[0];
            $(hfoutfallX).val(pt.x);
            $(hfoutfallY).val(pt.y);
        });

        /*End converting the x and y to delaware state plane*/


        var att = {
            "name": "New",
            "lat": latlong.y,
            "lon": latlong.x
        };

        mapPoint1(newOutfallLayer, evt.mapPoint, att, infoSymbol, true)

        //var content = "Latitude = ${y} <br/> Longitude = ${x}";
        //myMapView.infoWindow.setContent(esri.substitute(webMercatorUtils.webMercatorToGeographic(evt.mapPoint), content));
        //myMapView.infoWindow.show(evt.screenPoint, myMapView.getInfoWindowAnchor(evt.screenPoint));


        WaterShedQuery1(evt.mapPoint);
        
    });
}

function WaterShedQuery1(point) {
    require(["esri/tasks/support/Query", "esri/tasks/QueryTask"], function (Query, QueryTask) {
        var queryTask1;
        var query1 = new Query();
        queryTask1 = new QueryTask("https://enterprise.firstmap.delaware.gov/arcgis/rest/services/Hydrology/DE_Watersheds/MapServer/5")
        query1.returnGeometry = true;
        query1.outFields = ["*"];
        query1.geometry = point;
        queryTask1.execute(query1).then(WaterShedResults1);
    });
}

function WaterShedResults1(featureSet) {


    if (featureSet.features.length > 0) {

        $(txtoutfallWatershed).val(featureSet.features[0].attributes["NAME"]);
        $(hfoutfallWaterShedCode).val(featureSet.features[0].attributes["HUC12"]);
        
    }
    else {

        var centerpoint = myMapView.center.clone();

        myMapView.popup.open({
            title: "Warning!",
            content: "Unable to locate the Watershed.",
            location: centerpoint
        });
        //alert('Unable to locate the Watershed.');
    }
}
