import Link from "next/link";
import { Button } from "../ui/button";
import { getSession, logout } from "@/app/lib/actions";
import { redirect } from "next/navigation";

export default async function NavbarButton() {
  const session = await getSession();
  return (
    <>
      {session ? (
        <form
          action={async () => {
            "use server";
            await logout();
            redirect("/");
          }}
        >
          <Button variant="outline" type="submit">
            Logout
          </Button>
        </form>
      ) : (
        <Link href="/auth/login">
          <Button>Login</Button>
        </Link>
      )}
    </>
  );
}
