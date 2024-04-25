"use client";

import * as React from "react";
import { cn } from "@/lib/utils";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Github, Loader2 } from "lucide-react";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";

import { useFormState, useFormStatus } from "react-dom";
import loginAction from "./loginAction";
import Link from "next/link";

const loginFormSchema = z.object({
  email: z.string({
    required_error: "An email is required.",
  }),
  password: z.string({
    required_error: "A password is required.",
  }),
});

type LoginFormValues = z.infer<typeof loginFormSchema>;

// This can come from your database or API.
const defaultValues: Partial<LoginFormValues> = {
  // name: "Your name",
  // dob: new Date("2023-01-23"),
};

interface UserAuthFormProps extends React.HTMLAttributes<HTMLDivElement> {}

export function UserAuthForm({ className, ...props }: UserAuthFormProps) {
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [errorMessage, dispatch] = useFormState(loginAction, undefined);

  const form = useForm<LoginFormValues>({
    resolver: zodResolver(loginFormSchema),
    defaultValues,
  });

  async function onSubmit(event: React.SyntheticEvent) {
    event.preventDefault();
    setIsLoading(true);

    setTimeout(() => {
      setIsLoading(false);
    }, 3000);
  }

  return (
    <div className={cn("grid gap-6", className)} {...props}>
      <form action={dispatch}>
        <div className="grid gap-4">
          <div className="grid gap-2">
            <Label htmlFor="email">Email</Label>
            <Input
              id="email"
              placeholder="name@example.com"
              type="email"
              name="email"
              autoCapitalize="none"
              autoComplete="email"
              autoCorrect="off"
              disabled={isLoading}
            />
          </div>{" "}
          <div className="grid gap-2">
            <Label htmlFor="password">Password</Label>
            <Input
              id="password"
              placeholder="Password"
              type="password"
              name="password"
              autoComplete="current-password"
              disabled={isLoading}
            />
          </div>
          <p className="text-sm text-muted-foreground flex justify-end gap-2">
            <Link href="/auth/reset-password" className="text-primary hover:underline">
              Forgot password?
            </Link>
          </p>
          <div>
            {errorMessage && (
              <p className="text-red-600 dark:text-blue-800">{errorMessage}</p>
            )}
          </div>
          <LoginButton />
        </div>
      </form>
      <div className="relative">
        <div className="absolute inset-0 flex items-center">
          <span className="w-full border-t" />
        </div>
        <div className="relative flex justify-center text-xs uppercase">
          <span className="bg-background px-2 text-muted-foreground">
            Or continue with
          </span>
        </div>
      </div>
      <Button variant="outline" type="button" disabled={isLoading}>
        {isLoading ? (
          <Loader2 className="mr-2 h-4 w-4 animate-spin" />
        ) : (
          <Github className="mr-2 h-4 w-4" />
        )}{" "}
        GitHub
      </Button>
    </div>
  );
}

function LoginButton() {
  const { pending } = useFormStatus();

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    if (pending) {
      event.preventDefault();
    }
  };

  return (
    <Button aria-disabled={pending} type="submit" onClick={handleClick}>
      {pending && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
      Login
    </Button>
  );
}
