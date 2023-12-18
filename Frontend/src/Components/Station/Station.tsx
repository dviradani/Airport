import React from "react";
import IStation from "../../Interfaces/IStation";
import FlightIcon from "../FlightIcon/FlightIcon";
import './Station.css';

interface IStationProps {
  stationIndex : number;
  stations : IStation[] | null;
  cssStationClass : string;
  name : string;
}

const Station = ({ stations , stationIndex  , cssStationClass ,name} : IStationProps) => {
  return (
    <>
      <div className={cssStationClass}>
        <h5>{name}</h5>
        <div className={stations && (stations[stationIndex]?.name === "Station 5" || stations[stationIndex]?.name === "Station 6" || stations[stationIndex]?.name === "Station 7" || stations[stationIndex]?.name === "Station 8")  ? "station-bg-inner" : ""}>
            {stations && stations[stationIndex]?.flight ? <FlightIcon cssIndex={stationIndex+1} isDeparting={stations[stationIndex].flight.isDeparting} /> : null}
            {stations && stations[stationIndex]?.flight?.flightNumber} <br />
            {stations && stations[stationIndex]?.flight?.name}
        </div>
        
      </div>
    </>
  );
};

export default Station;
