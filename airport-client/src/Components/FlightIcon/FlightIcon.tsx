import React from "react";
import IFlight from "../../Interfaces/IFlight";
import './FlightIcon.css';


interface IFlightIconProps {
  cssIndex : number;
  isDeparting : boolean;
}

const FlightIcon = ({ cssIndex , isDeparting } : IFlightIconProps) => {
  let cssClasses : string = '';

  if (cssIndex === 1 || cssIndex === 2 || cssIndex === 3){
      cssClasses = 'left arriving';
  }
  if(cssIndex === 4)
  {
    if(isDeparting) {
      cssClasses = 'left departing'
    }
    else {
      cssClasses = 'down arriving'
    }
  }
  if (cssIndex === 5){
    cssClasses = 'down arriving';
  }
  if (cssIndex === 8){
    cssClasses = 'up departing';
  }
  if (cssIndex === 6 || cssIndex === 7){
    if (isDeparting){
      cssClasses = 'up departing';
    }
    else {
      cssClasses = 'down arriving';
    }
  }

  return (
    <>
      <div className={cssClasses}>
        <span className="material-symbols-outlined">flight</span>
      </div>
    </>
  );
};

export default FlightIcon;
