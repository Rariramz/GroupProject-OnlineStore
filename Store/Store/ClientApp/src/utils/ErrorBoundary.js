import React, { Box, Typography } from "react";

class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { error: null, errorInfo: null };
  }

  componentDidCatch(error, errorInfo) {
    // Catch errors in any components below and re-render with error message
    this.setState({
      error: error,
      errorInfo: errorInfo,
    });
    // You can also log error messages to an error reporting service here
  }

  render() {
    if (this.state.errorInfo) {
      // Error path
      return (
        <Box padding="40vh">
          <Typography variant="h1" color="primary" textAlign="center">
            Something went wrong...
          </Typography>
        </Box>
      );
    }
    return this.props.children;
  }
}

export default ErrorBoundary;
