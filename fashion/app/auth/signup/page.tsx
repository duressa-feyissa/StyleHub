import Link from "next/link";
import { UserAuthForm } from "./use-auth-form";

export default function Login() {
  return (
    <>
      <div className="lg:p-8 h-screen  content-center">
        <div className="mx-auto flex w-full flex-col justify-center space-y-6 sm:w-[350px] p-5">
          <div className="flex flex-col space-y-2 text-center">
            <h1 className="text-2xl font-normal">Create an account</h1>
            <p className="text-sm text-muted-foreground">
              Enter your email below to create your account
            </p>
          </div>
          <UserAuthForm />
          <p className="text-sm text-muted-foreground flex justify-end gap-2">
            Already have an account?{" "}
            <Link href="/auth/login" className="text-primary hover:underline">
              Log in
            </Link>
          </p>
          <p className="px-8 text-center text-sm text-muted-foreground">
            By clicking continue, you agree to our{" "}
            <Link
              href="/terms"
              className="underline underline-offset-4 hover:text-primary"
            >
              Terms of Service
            </Link>{" "}
            and{" "}
            <Link
              href="/privacy"
              className="underline underline-offset-4 hover:text-primary"
            >
              Privacy Policy
            </Link>
            .
          </p>
        </div>
      </div>
    </>
  );
}
