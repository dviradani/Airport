import React from "react";
import IFlight from "../../Interfaces/IFlight";

interface IUpdateTableProps {
  planeListData: IFlight[] | null;
}

const UpdateTable = ({ planeListData }: IUpdateTableProps) => {
  return (
    <>
      <div className="table-container">
        <div className="arrival-table tbl">
          <h3>
            <u>Arrivals Updates</u>
          </h3>
          <div>
            <ul>{planeListData?.map(checkArriving)}</ul>
          </div>
        </div>
        <div className="departure-table tbl">
          <h3>
            <u>Departures Updates</u>
          </h3>
          <div>
            <ul>{planeListData?.map(checkDeparturing)}</ul>
          </div>
        </div>
      </div>
    </>
  );
};

function checkArriving(flight: IFlight) {
  if (flight != null) {
    return !flight.isDeparting ? (
      <li>{flight.flightNumber + "    " + flight.name}</li>
    ) : null;
  } else {
    return null;
  }
}

function checkDeparturing(flight: IFlight) {
  if (flight != null) {
    return flight.isDeparting ? (
      <li>{flight.flightNumber + " " + flight.name}</li>
    ) : null;
  } else {
    return null;
  }
}

export default UpdateTable;
