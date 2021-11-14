/**
 * Component which creates a custom check box.
 * @author Andrew Jarombek
 * @since 11/14/2021
 */

import React from 'react';
import { createUseStyles } from 'react-jss';
import styles from './styles';
import classNames from 'classnames';
import PropTypes from 'prop-types';

const useStyles = createUseStyles(styles);

const CheckBox = ({ id, checked, onChange, className }) => {
  const classes = useStyles();

  return (
    <div className={classNames(classes.checkBox, className)} onClick={onChange}>
      <input type="checkbox" id={id} className={classes.input} checked={checked} />
      <span>{checked && <p>&#x4e;</p>}</span>
    </div>
  );
};

CheckBox.propTypes = {
  id: PropTypes.string,
  checked: PropTypes.bool.isRequired,
  onChange: PropTypes.func.isRequired,
  className: PropTypes.string
};

export default CheckBox;
