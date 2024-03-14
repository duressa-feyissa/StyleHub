import React from "react";

type props = {
  className: string;
  Name: string;
};

const Button = ({ className, Name }: props) => {
  return (
    <div>
      <button className={className}>{Name}</button>
    </div>
  );
};

export default Button;
