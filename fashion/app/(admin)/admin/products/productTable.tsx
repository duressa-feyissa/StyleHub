"use client"

import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import Image from "next/image";
import { useEffect, useState } from "react";
import {
  DropdownMenu,
  DropdownMenuCheckboxItem,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import { MoreHorizontal } from "lucide-react";
import { Badge } from "@/components/ui/badge";
import { ProductType } from "@/lib/type";
import { useGetProducts } from "@/lib/data/get-products";

async function getData() {
  const res = await fetch("/api/products", { cache: "no-store" });

  if (!res.ok) {
    // This will activate the closest `error.js` Error Boundary
    throw new Error("Failed to fetch data");
  }

  return res.json();
}

export default function ProductsTable() {
  const { data: products, error: postError, fetchStatus } = useGetProducts()
  if (postError || !products) return postError?.message
  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead className="hidden w-[100px] sm:table-cell">
            <span className="sr-only">Image</span>
          </TableHead>
          <TableHead>Name</TableHead>
          <TableHead>Status</TableHead>
          <TableHead className="hidden md:table-cell">Price</TableHead>
          <TableHead className="hidden md:table-cell">Total Sales</TableHead>
          <TableHead className="hidden md:table-cell">Created at</TableHead>
          <TableHead>
            <span className="sr-only">Actions</span>
          </TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {products.map((product: ProductType) => (
          <TableRow key={product.id}>
            <TableCell className="hidden sm:table-cell">
              <Image
                alt="Product image"
                className="aspect-square rounded-md"
                height="64"
                src="/vercel.svg"
                width="64"
              />
            </TableCell>
            <TableCell className="font-medium">{product.title}</TableCell>
            <TableCell>
              <Badge variant="outline">{product.target}</Badge>
            </TableCell>
            <TableCell className="hidden md:table-cell">
              ${product.price}
            </TableCell>
            <TableCell className="hidden md:table-cell">
              {product.quantity}
            </TableCell>
            <TableCell className="hidden md:table-cell">
              {new Date(product.createdAt).toLocaleDateString()}
            </TableCell>
            <TableCell>
              <DropdownMenu>
                <DropdownMenuTrigger asChild>
                  <Button aria-haspopup="true" size="icon" variant="ghost">
                    <MoreHorizontal className="h-4 w-4" />
                    <span className="sr-only">Toggle menu</span>
                  </Button>
                </DropdownMenuTrigger>
                <DropdownMenuContent align="end">
                  <DropdownMenuLabel>Actions</DropdownMenuLabel>
                  <DropdownMenuItem>Edit</DropdownMenuItem>
                  <DropdownMenuItem>Delete</DropdownMenuItem>
                </DropdownMenuContent>
              </DropdownMenu>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
