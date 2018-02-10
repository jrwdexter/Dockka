import * as React from 'react';

export interface GridProps {
  children?: Cell | Cell[];
}

export const Grid = (props: GridProps) => (
  <div className="mdc-layout-grid">
    <div className="mdc-layout-grid--inner">
      {props.children}
    </div>
  </div>
);

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
    let { col, desktop, tablet, phone, index, fixedWidth, align, gridPosition } = this.props;
    return (
      <div className={`mdc-layout-grid__cell
                       ${col ? ` mdc-layout-grid__cell--span-${col}` : ''}
                       ${desktop ? ` mdc-layout-grid__cell--span-${desktop}-desktop` : ''}
                       ${tablet ? ` mdc-layout-grid__cell--span-${tablet}-tablet` : ''}
                       ${phone ? ` mdc-layout-grid__cell--span-${phone}-phone` : ''}
                       ${index ? ` mdc-layout-grid__cell--span-${index}` : ''}
                       ${fixedWidth ? ` mdc-layout-grid__cell--span-${col}` : ''}
                       ${align ? ` mdc-layout-grid__cell--span-${align}` : ''}
                       ${gridPosition ? ` mdc-layout-grid__cell--span-${gridPosition}` : ''}
                      `}></div>
    );
  }
}


export default Grid;