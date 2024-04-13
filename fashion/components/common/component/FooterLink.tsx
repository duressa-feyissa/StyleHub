import Link from "next/link";

interface FooterLinkProps {
  name: string;
  isHeading: boolean;
}

const FooterLink = ({ name, isHeading }: FooterLinkProps) => {
  return (
    <>
      {isHeading ? (
        <p className="text-onPrimary text-md font-bold">{name}</p>
      ) : (
        <Link href="#">
          <p className="text-primaryContainer prose-body-large opacity-80 hover:opacity-100 hover:text-primary text-sm">
            {name}
          </p>
        </Link>
      )}
    </>
  );
};

export default FooterLink;
