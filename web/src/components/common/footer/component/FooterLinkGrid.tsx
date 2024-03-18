import FooterLink from "./FooterLink";

function FooterLinkGrid() {
  const array = [
    ["About", "Contact", "Careers", "Press", "Blog", "Affiliates"],
    [
      "Company",
      "Privacy Policy",
      "Terms of Service",
      "Refund Policy",
      "Partners",
    ],
    ["Social", "Facebook", "Twitter", "Instagram", "Pinterest"],
  ];

  return (
    <div className=" flex flex-row gap-x-v-large justify-start items-start">
      {array.map((item, index) => {
        return (
          <div key={index} className="flex flex-col gap-y-small">
            {item.map((subItem, subIndex) => {
              return (
                <FooterLink
                  key={subIndex}
                  name={subItem}
                  isHeading={subIndex === 0}
                />
              );
            })}
          </div>
        );
      })}
    </div>
  );
}

export default FooterLinkGrid;
