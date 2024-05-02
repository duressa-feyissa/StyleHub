"use client";

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
import { deleteProduct } from "@/server/actions/add-product";

async function getData() {
  const res = await fetch("/api/products", { cache: "no-store" });

  if (!res.ok) {
    // This will activate the closest `error.js` Error Boundary
    throw new Error("Failed to fetch data");
  }

  return res.json();
}

export default function ProductsTable() {
  async function handleDelete(id: string) {
    const res = await deleteProduct(id);
    console.log(res);
  }

  const { data: products, error: postError, fetchStatus } = useGetProducts();
  if (postError || !products) return postError?.message;
  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead className="hidden w-[100px] sm:table-cell">
            <span className="sr-only">Image</span>
          </TableHead>
          <TableHead>Name</TableHead>
          <TableHead>Condition</TableHead>
          <TableHead className="hidden md:table-cell">Price</TableHead>
          <TableHead className="hidden md:table-cell">Quantity</TableHead>
          <TableHead className="hidden md:table-cell">Target</TableHead>
          <TableHead className="hidden md:table-cell">City</TableHead>
          <TableHead className="hidden md:table-cell">Brand</TableHead>
          <TableHead className="hidden md:table-cell">Colors</TableHead>
          <TableHead className="hidden md:table-cell">Materials</TableHead>
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
                alt={product.title}
                className="aspect-square rounded-md"
                height="64"
                src={product?.images[0]?.imageUrl}
                width="64"
              />
            </TableCell>
            <TableCell className="font-medium">{product.title}</TableCell>
            <TableCell>
              <Badge variant="outline">{product.condition}</Badge>
            </TableCell>
            <TableCell className="hidden md:table-cell">
              {product.price} ETB
            </TableCell>
            <TableCell className="hidden md:table-cell">
              <Badge variant="outline">{product.quantity}</Badge>
            </TableCell>
            <TableCell className="hidden md:table-cell">
              {product.target}
            </TableCell>
            <TableCell className="hidden md:table-cell">
              {product.city}
            </TableCell>
            <TableCell className="hidden md:table-cell">
              {product.brand.name}
            </TableCell>
            <TableCell className="hidden md:table-cell">
              <div className="flex">
                {product.colors.map((color) => (
                  <div
                    key={color.id}
                    className="w-4 h-4 border border-opacity-25 border-onSurface rounded-full"
                    style={{ backgroundColor: color.hexCode }}
                  />
                ))}
              </div>
            </TableCell>
            <TableCell className="hidden md:table-cell">
              {product.materials.map((material) => (
                <Badge key={material.id} variant="outline">
                  {material.name}
                </Badge>
              ))}
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
                  <DropdownMenuItem
                    onClick={() =>
                      confirm(
                        "Are you sure you want to delete this product?"
                      ) && handleDelete(product.id)
                    }
                  >
                    Delete
                  </DropdownMenuItem>
                </DropdownMenuContent>
              </DropdownMenu>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
