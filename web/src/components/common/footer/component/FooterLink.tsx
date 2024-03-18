import Link from "next/link";

interface FooterLinkProps {
  name: string;
  isHeading: boolean;
}

const FooterLink = ({ name, isHeading }: FooterLinkProps) => {
  return (
    <>
      {isHeading ? (
        <p className="text-onPrimary prose-headline-small ">{name}</p>
      ) : (
        <Link href="#">
          <p className="text-primaryContainer prose-body-large opacity-80 hover:opacity-100 hover:text-primary">
            {name}
          </p>
        </Link>
      )}
    </>
  );
};

export default FooterLink;
