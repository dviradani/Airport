import React from "react";
import { useState, useEffect } from "react";
import IFlight from "../../Interfaces/IFlight";
import IStation from "../../Interfaces/IStation";
import * as signalR from "@microsoft/signalr";
import UpdateTable from "../UpdateTable/UpdateTable";
import Station from "../Station/Station";
import "./Simulator.css";
import VideoPreview from "../VideoPreview/VideoPreview";
import axios from "axios";

const Airport = () => {
  const [routeData, setrouteData] = useState<IStation[] | null>(null);
  const [planeListData, setPlanesDate] = useState<IFlight[] | null>(null);
  const [isStarted, setIsStarted] = useState(false);

  const baseUrl = 'http://localhost:5165/'

  const handleClick = () => {
    fetch( baseUrl +"api/airport/start");
    setIsStarted(true);
  };

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(baseUrl + "hub/")
      .build();

    const handlerouteData = async (routeData: IStation[]) => {
      setrouteData(routeData);
      console.log(routeData);
    };

    const getPlanesData = async (planeListData: IFlight[]) => {
      setPlanesDate(planeListData);
      console.log(planeListData);
    };

    axios.get(baseUrl + 'api/airport/status').then(response => {
         setIsStarted(response.data)});

    connection.start().then(() => {
      console.log("SignalR connection established.");
      connection.on("GetRoute", handlerouteData);
      connection.on("GetFlights", getPlanesData);
    });
  }, []);
  return (
    <>
      <div>
        <div className="main-container">
          <div className="title-container">
            <div className="title">
                <VideoPreview/>
            </div>
          </div>
          <div className="mid-container">
            <div className="stations-container">
              <div className="station-row-1-container">
                <div className="station-4-9-container">
                  <Station
                    name="Station 4"
                    stations={routeData}
                    stationIndex={3}
                    cssStationClass="station-mid basic-station"
                  />
                </div>
                <div className="station-3-2-1-container">
                  <Station
                    name="Station 3"
                    stations={routeData}
                    stationIndex={2}
                    cssStationClass="station-sm basic-station"
                  />
                  <Station
                    name="Station 2"
                    stations={routeData}
                    stationIndex={1}
                    cssStationClass="station-sm basic-station"
                  />
                  <Station
                    name="Station 1"
                    stations={routeData}
                    stationIndex={0}
                    cssStationClass="station-sm basic-station"
                  />
                </div>
              </div>
              <div className="station-row-2-container">
                <div className="station-5-8-container">
                  <Station
                    name="Station 5"
                    stations={routeData}
                    stationIndex={4}
                    cssStationClass="station-bg basic-station"
                  />
                  <Station
                    name="Station 8"
                    stations={routeData}
                    stationIndex={7}
                    cssStationClass="station-bg basic-station"
                  />
                </div>
              </div>
              <div className="station-row-3-container">
                <div className="station-6-7-container">
                  <Station
                    name="Station 6"
                    stations={routeData}
                    stationIndex={5}
                    cssStationClass="station-bg basic-station"
                  />
                  <Station
                    name="Station 7"
                    stations={routeData}
                    stationIndex={6}
                    cssStationClass="station-bg basic-station"
                  />
                </div>
              </div>
            </div>
            <UpdateTable planeListData={planeListData} />
          </div>
          <div className="btn-container">
             {isStarted ?  <h3>On Air</h3>: <button onClick={handleClick}>Start</button>} 
          </div>
        </div>
      </div>
    </>
  );
};

export default Airport;
