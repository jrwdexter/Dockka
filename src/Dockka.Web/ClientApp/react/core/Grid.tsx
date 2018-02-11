import * as React from "react";
import * as classnames from "classnames";
var gridStyles = require("@material/layout-grid/mdc-layout-grid.scss");

export interface GridProps {
  children: any;
}

export const Grid = (props: GridProps) => {
  console.log(gridStyles);
  return (
    <div className={gridStyles["mdc-layout-grid"]}>
      <div className={gridStyles["mdc-layout-grid__inner"]}>
        {props.children}
      </div>
    </div>
  );
};

type CellColumn = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12;
type CellAlign = "top" | "middle" | "bottom";
type CellPosition = "left" | "right";

export interface CellProps {
  col?: CellColumn;
  desktop?: CellColumn;
  tablet?: CellColumn;
  phone?: CellColumn;
  index?: CellColumn;
  fixedWidth?: boolean;
  align?: CellAlign;
  gridPosition?: CellPosition;
}

export class Cell extends React.PureComponent<CellProps> {
  render() {
    let {
      col,
      desktop,
      tablet,
      phone,
      index,
      fixedWidth,
      align,
      gridPosition
    } = this.props;
    let className = classnames(gridStyles["mdc-layout-grid__cell"], {
      [gridStyles[`mdc-layout-grid__cell--span-${col}`]]: !!col,
      [gridStyles[`mdc-layout-grid__cell--span-${desktop}-desktop`]]: !!desktop,
      [gridStyles[`mdc-layout-grid__cell--span-${tablet}-tablet`]]: !!tablet,
      [gridStyles[`mdc-layout-grid__cell--span-${phone}-phone`]]: !!phone,
      [gridStyles[`mdc-layout-grid__cell--span-${index}`]]: !!index,
      [gridStyles[`mdc-layout-grid__cell--span-${align}`]]: !!align,
      [gridStyles[`mdc-layout-grid__cell--span-${gridPosition}`]]: !!gridPosition
    });
    return <div className={className}>{this.props.children}</div>;
  }
}

export default Grid;
