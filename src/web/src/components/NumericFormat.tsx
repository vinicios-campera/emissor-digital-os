import React from "react";
import { NumericFormat, NumericFormatProps } from "react-number-format";

interface CustomProps {
  onChange: (event: { target: { name: string; value: string } }) => void;
  name: string;
  removePrefix?: boolean;
}

export const NumericFormatCustom = React.forwardRef<
  NumericFormatProps,
  CustomProps
>(function NumericFormatCustom(props, ref) {
  const { onChange, removePrefix, ...other } = props;

  return (
    <NumericFormat
      {...other}
      getInputRef={ref}
      onValueChange={(values) => {
        onChange({
          target: {
            name: props.name,
            value: values.value,
          },
        });
      }}
      thousandSeparator="."
      decimalSeparator=","
      allowNegative={false}
      valueIsNumericString
      allowLeadingZeros
      decimalScale={2}
      prefix={removePrefix ? "" : "R$ "}
    />
  );
});
