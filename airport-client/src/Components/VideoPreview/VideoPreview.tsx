import React from "react";
import introVideo from './Airport Simulator (1).mp4';


const VideoPreview = () => {
  return (
        <video autoPlay muted>
          <source src={introVideo} type="video/mp4" />
          Your browser does not support the video tag.
        </video>

  );
}


export default VideoPreview;