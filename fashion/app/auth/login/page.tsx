import Link from "next/link";
import { UserAuthForm } from "./use-auth-form";

export default async function Login() {
  return (
    <>
      <div className="lg:p-8 h-screen  content-center">
        <div className="mx-auto flex w-full flex-col justify-center space-y-6 sm:w-[350px] p-5">
          <div className="flex flex-col space-y-2 text-center">
            <h1 className="text-2xl font-normal tracking-tight">
              Log in to your account
            </h1>
            <p className="text-sm text-muted-foreground">
              Enter your email and password to continue
            </p>
          </div>
          <UserAuthForm />
          <p className="text-sm text-muted-foreground flex justify-end gap-2">
            Don&apos;t have an account?{" "}
            <Link href="/auth/signup" className="text-primary hover:underline">
              Sign up
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
