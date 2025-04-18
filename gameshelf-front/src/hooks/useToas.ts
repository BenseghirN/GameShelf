import { useState } from "react";

export function useToast() {
  const [toast, setToast] = useState<{
    open: boolean;
    message: string;
    severity: "success" | "error" | "info" | "warning";
  }>({
    open: false,
    message: "",
    severity: "info",
  });

  const showToast = (
    message: string,
    severity: "success" | "error" | "info" | "warning" = "info"
  ) => {
    setToast({ open: true, message, severity });
  };

  const closeToast = () => {
    setToast({ ...toast, open: false });
  };

  return { toast, showToast, closeToast };
}
